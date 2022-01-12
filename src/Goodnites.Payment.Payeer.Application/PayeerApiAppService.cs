using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.PaymentService.Payments;
using Goodnites.Payment.Payeer.Payeer;
using Goodnites.Payment.Payeer.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Encryption;
using Volo.Abp.SettingManagement;
using Volo.Abp.Users;

namespace Goodnites.Payment.Payeer
{
    public class PayeerApiAppService : IPayeerApiAppService
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;
        private readonly ICurrentUser _currentUser;
        private readonly PayeerApi _payeerApi;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IPayeerPaymentRepository _payeerPaymentRepository;
        private readonly IPaymentManager _paymentManager;
        private readonly ISettingManager _settingManager;
        private readonly IStringEncryptionService _stringEncryptionService;

        public PayeerApiAppService(
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            PayeerApi payeerApi,
            ICurrentUser currentUser,
            IDistributedEventBus distributedEventBus,
            IPayeerPaymentRepository payeerPaymentRepository,
            IPaymentManager paymentManager,
            ISettingManager settingManager,
            IStringEncryptionService stringEncryptionService)
        {
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _payeerApi = payeerApi;
            _currentUser = currentUser;
            _distributedEventBus = distributedEventBus;
            _payeerPaymentRepository = payeerPaymentRepository;
            _paymentManager = paymentManager;
            _settingManager = settingManager;
            _stringEncryptionService = stringEncryptionService;
        }


        public async Task<string> CreateChargeUrlAsync(string amountStr, string currency, string description)
        {
            if (decimal.TryParse(amountStr, out decimal amount) == false)
            {
                throw new UserFriendlyException("Your amount is invalid");
            }

            await CheckMinMaxValuesAsync(amount).ConfigureAwait(false);

            var payeerPaymentId = _guidGenerator.Create();

            var payeerRequest = new PayeerModel(
                paymentId: payeerPaymentId.ToString(),
                amount: amount.ToString(CultureInfo.InvariantCulture),
                currency: currency,
                description: description
            );

            var url = await _payeerApi.CreateChargeRequestUrlAsync(payeerRequest);

            var createPaymentEto = new CreatePaymentEto(
                _currentTenant.Id,
                _currentUser.GetId(),
                PayeerConsts.Payeer,
                currency,
                new List<CreatePaymentItemEto>()
                {
                    new CreatePaymentItemEto
                    {
                        ItemType = "FUND",
                        ItemKey = payeerPaymentId.ToString(),
                        OriginalPaymentAmount = Convert.ToDecimal(amount),
                    }
                }
            );

            createPaymentEto.SetProperty("FUND", true);
            createPaymentEto.SetProperty("PayeerPaymentId", payeerPaymentId);
            createPaymentEto.SetProperty("PaymentMethod", PayeerConsts.Payeer);
            createPaymentEto.SetProperty("PayeerRequest", payeerRequest);

            await _distributedEventBus.PublishAsync(createPaymentEto);

            return url;
        }

        private async Task CheckMinMaxValuesAsync(decimal amount)
        {
            var min =
                await _settingManager.GetOrNullAsync(PayeerSettings.PayeerMin, "G", null, true);

            var max = await _settingManager.GetOrNullAsync(PayeerSettings.PayeerMax, "G", null, true);


            if (decimal.Parse(min) <= amount && amount <= decimal.Parse(max))
            {
            }
            else
            {
                throw new UserFriendlyException($"Accepted Values Between {min} - {max}");
            }
        }

// https://www.payeer.com/upload/pdf/PayeerMerchanten.pdf 
// returned 'success' or 'error'
        public async Task WebHookAsync(PayeerResponseModel payeerResponseModel, string clientIp)
        {
            var ipIsValid = await _payeerApi.ClientIpIsValidAsync(clientIp);

            if (ipIsValid == false)
            {
                throw new UserFriendlyException("Invalid Client Ip");
            }

            var additionalParameters = JsonConvert.DeserializeObject<JObject>(DecryptAdditionalParameters(payeerResponseModel.AdditionalParameters));
            var userId = additionalParameters[PayeerConsts.UserId].ToString();


            var payeerPayments =
                await _payeerPaymentRepository.GetPayeerPaymentsByUserIdAsync(Guid.Parse(userId));

            var payment =
                payeerPayments.FirstOrDefault(
                    x => x.GetProperty("PayeerPaymentId", "") == payeerResponseModel.PaymentId);


            if (payment.GetProperty(payeerResponseModel.PaymentStatus, "").IsNullOrEmpty() == false)
            {
                // already processed
                return;
            }

            if (payeerResponseModel.PaymentStatus == "success")
            {
                var extraPropertyDictionary = new ExtraPropertyDictionary()
                {
                    {PayeerConsts.UserId, userId},
                    {PayeerConsts.PayeerStatus, payeerResponseModel.PaymentStatus}
                };

                await _paymentManager.StartPaymentAsync(payment, extraPropertyDictionary);
            }
            else if (payeerResponseModel.PaymentStatus == "error")
            {
                await _paymentManager.StartCancelAsync(payment);
            }
        }

        private string DecryptAdditionalParameters(string base64cryptedText)
        {
            var encryptedText = Encoding.UTF8.GetString(Convert.FromBase64String(base64cryptedText));

            var text = _stringEncryptionService.Decrypt(encryptedText);

            return text;
        }
    }
}
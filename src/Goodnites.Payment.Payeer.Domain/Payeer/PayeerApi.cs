using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Goodnites.Payment.Payeer.Settings;
using Newtonsoft.Json;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Encryption;
using Volo.Abp.SettingManagement;
using Volo.Abp.Users;

namespace Goodnites.Payment.Payeer.Payeer
{
    public class PayeerApi : ITransientDependency
    {
        private readonly ISettingManager _settingManager;
        private readonly ICurrentUser _currentUser;
        private readonly IStringEncryptionService _stringEncryptionService;

        public const string ApiChargeUrl = "https://payeer.com/merchant/";

        public const string SenderIps = "https://payeer.com/merchant/ips.txt";

        public PayeerApi(ISettingManager settingManager, ICurrentUser currentUser,
            IStringEncryptionService stringEncryptionService)
        {
            _settingManager = settingManager;
            _currentUser = currentUser;
            _stringEncryptionService = stringEncryptionService;
        }


        public async Task<bool> ClientIpIsValidAsync(string clientIp)
        {
            var ips = await SenderIps.GetStringAsync();

            var ipIsValid = ips.Split(";").Contains(clientIp);

            return ipIsValid;
        }

        // example url : https://payeer.com/merchant/?m_shop=1282426503&m_orderid=5782&m_amount=5.00&m_curr=USD&m_desc=QmFsYW5jZSByZWNoYXJnZSAodGVzdDAxKQ%3D%3D&m_sign=CD0D7F2A7EE3CCC2D9B66BD49B4EA519F3116FA09488B107A01CA3EAF471B068
        public async Task<string> CreateChargeRequestUrlAsync(PayeerModel charge, string additionalParams = null)
        {
            if (charge.HashCreated == false)
            {
                await SetMerchantIdAndSecretKeyAsync(charge);
            }

            if (additionalParams.IsNullOrEmpty() == false)
            {
                var encryptedParams = _stringEncryptionService.Encrypt(additionalParams);

                charge.AdditionalParamaters = Convert.ToBase64String(Encoding.UTF8.GetBytes(encryptedParams));
            }

            var url =
                ApiChargeUrl
                    .SetQueryParams(new
                    {
                        m_shop = charge.MerchantId,
                        m_orderid = charge.PaymentId,
                        m_amount = charge.Amount,
                        m_curr = charge.Currency,
                        m_desc = charge.Description,
                        m_sign = charge.Hash
                    });

            if (additionalParams.IsNullOrEmpty() == false)
            {
                url.SetQueryParams(new {m_params = charge.AdditionalParamaters});
            }

            return url;
        }

        private async Task SetMerchantIdAndSecretKeyAsync(PayeerModel charge)
        {
            var merchantId = await _settingManager.GetOrNullAsync(PayeerSettings.PayeerMerchantId, "G", null);
            var secretKey = await _settingManager.GetOrNullAsync(PayeerSettings.PayeerSecretKey, "G", null);

            if (merchantId.IsNullOrEmpty() || secretKey.IsNullOrEmpty())
                throw new UserFriendlyException("Please fill  merchantId or secret key on Settings Page");

            charge.SetMerchantId(merchantId);
            charge.SetSecretKey(secretKey);
            charge.GenerateHash();
        }
    }
}
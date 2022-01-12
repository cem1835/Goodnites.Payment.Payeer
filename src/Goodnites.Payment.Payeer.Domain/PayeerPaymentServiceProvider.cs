using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.PaymentService.Payments;
using EasyAbp.PaymentService.Refunds;
using Volo.Abp.Data;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;

namespace Goodnites.Payment.Payeer
{
    public class PayeerPaymentServiceProvider:PaymentServiceProvider
    {
        private readonly IPaymentManager _paymentManager;
        private readonly IPaymentRepository _paymentRepository;

        public PayeerPaymentServiceProvider(IPaymentRepository paymentRepository, IPaymentManager paymentManager)
        {
            _paymentRepository = paymentRepository;
            _paymentManager = paymentManager;
        }

        public override async Task OnPaymentStartedAsync(EasyAbp.PaymentService.Payments.Payment payment, ExtraPropertyDictionary configurations)
        {
            if (payment.ActualPaymentAmount <= decimal.Zero)
            {
                throw new PaymentAmountInvalidException(payment.ActualPaymentAmount, PayeerConsts.Payeer);
            }
            
            if (!Guid.TryParse(configurations.GetOrDefault(PayeerConsts.UserId) as string, out var accountId))
            {
                throw new ArgumentNullException(PayeerConsts.UserId);
            }
            
            
            await _paymentManager.CompletePaymentAsync(payment); // this func publish PaymentCompletedEto 

            await _paymentRepository.UpdateAsync(payment, true);
            
        }

        public override async Task OnCancelStartedAsync(EasyAbp.PaymentService.Payments.Payment payment)
        {
            if (payment.CanceledTime.HasValue)
            {
                return;
            }
            
            await _paymentManager.CompleteCancelAsync(payment);
        }
    }
}
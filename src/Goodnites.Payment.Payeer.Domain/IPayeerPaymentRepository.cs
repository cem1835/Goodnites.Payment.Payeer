using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Goodnites.Payment.Payeer
{
    public interface IPayeerPaymentRepository:ITransientDependency
    {
        Task<List<EasyAbp.PaymentService.Payments.Payment>> GetPayeerPaymentsByUserIdAsync(Guid userId);
    }
}
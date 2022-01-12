using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.PaymentService.EntityFrameworkCore;
using EasyAbp.PaymentService.Payments;
using Volo.Abp.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Goodnites.Payment.Payeer
{
    public class PayeerPaymentRepository : PaymentRepository, IPayeerPaymentRepository
    {
        public PayeerPaymentRepository(IDbContextProvider<IPaymentServiceDbContext> dbContextProvider) : base(
            dbContextProvider)
        {
        }

        public async Task<List<EasyAbp.PaymentService.Payments.Payment>> GetPayeerPaymentsByUserIdAsync(Guid userId)
        {
            var dbSet = await GetDbSetAsync();

            var payments = await dbSet.Where(x => x.UserId == userId && x.PaymentMethod == PayeerConsts.Payeer)
                .ToListAsync().ConfigureAwait(false);

            return payments;
        }
    }
}
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Goodnites.Payment.Payeer.EntityFrameworkCore
{
    [ConnectionStringName(PayeerDbProperties.ConnectionStringName)]
    public interface IPayeerDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}
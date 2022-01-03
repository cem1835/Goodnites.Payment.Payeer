using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Goodnites.Payment.Payeer.EntityFrameworkCore
{
    [ConnectionStringName(PayeerDbProperties.ConnectionStringName)]
    public class PayeerDbContext : AbpDbContext<PayeerDbContext>, IPayeerDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public PayeerDbContext(DbContextOptions<PayeerDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigurePayeer();
        }
    }
}
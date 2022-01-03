using Goodnites.Payment.Payeer.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Goodnites.Payment.Payeer
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(PayeerEntityFrameworkCoreTestModule)
        )]
    public class PayeerDomainTestModule : AbpModule
    {
        
    }
}

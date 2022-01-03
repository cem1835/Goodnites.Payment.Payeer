using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Goodnites.Payment.Payeer
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(PayeerDomainSharedModule)
    )]
    public class PayeerDomainModule : AbpModule
    {

    }
}

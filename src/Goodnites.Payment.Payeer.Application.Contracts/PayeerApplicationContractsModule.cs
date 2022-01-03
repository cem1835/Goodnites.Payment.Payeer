using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Goodnites.Payment.Payeer
{
    [DependsOn(
        typeof(PayeerDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class PayeerApplicationContractsModule : AbpModule
    {

    }
}

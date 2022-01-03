using Volo.Abp.Modularity;

namespace Goodnites.Payment.Payeer
{
    [DependsOn(
        typeof(PayeerApplicationModule),
        typeof(PayeerDomainTestModule)
        )]
    public class PayeerApplicationTestModule : AbpModule
    {

    }
}

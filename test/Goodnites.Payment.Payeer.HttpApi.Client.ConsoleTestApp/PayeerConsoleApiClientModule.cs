using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Goodnites.Payment.Payeer
{
    [DependsOn(
        typeof(PayeerHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class PayeerConsoleApiClientModule : AbpModule
    {
        
    }
}

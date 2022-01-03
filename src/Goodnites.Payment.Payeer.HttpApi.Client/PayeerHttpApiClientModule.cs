using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Goodnites.Payment.Payeer
{
    [DependsOn(
        typeof(PayeerApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class PayeerHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Payeer";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(PayeerApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}

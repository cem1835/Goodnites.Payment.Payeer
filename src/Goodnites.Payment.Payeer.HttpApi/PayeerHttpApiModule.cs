using Localization.Resources.AbpUi;
using Goodnites.Payment.Payeer.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Goodnites.Payment.Payeer
{
    [DependsOn(
        typeof(PayeerApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class PayeerHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(PayeerHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<PayeerResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}

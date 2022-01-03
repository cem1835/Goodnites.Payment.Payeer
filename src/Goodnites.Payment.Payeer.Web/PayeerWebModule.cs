using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Goodnites.Payment.Payeer.Localization;
using Goodnites.Payment.Payeer.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Goodnites.Payment.Payeer.Permissions;
using Goodnites.Payment.Payeer.Web.Settings;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.SettingManagement.Web.Pages.SettingManagement;

namespace Goodnites.Payment.Payeer.Web
{
    [DependsOn(
        typeof(PayeerHttpApiModule),
        typeof(AbpAspNetCoreMvcUiThemeSharedModule),
        typeof(AbpAutoMapperModule)
        )]
    public class PayeerWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(PayeerResource), typeof(PayeerWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(PayeerWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new PayeerMenuContributor());
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<PayeerWebModule>();
            });

            context.Services.AddAutoMapperObjectMapper<PayeerWebModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<PayeerWebModule>(validate: true);
            });

            Configure<RazorPagesOptions>(options =>
            {
                //Configure authorization.
            });
            
            Configure<SettingManagementPageOptions>(opt =>
            {
                opt.Contributors.Add(new PayeerSettingPageContributor());
                 
            });
             
            Configure<AbpBundlingOptions>(opt =>
            {
                opt.ScriptBundles.Configure(typeof(IndexModel).FullName,
                    configure =>
                    {
                        configure.AddFiles("/Pages/Components/PayeerSettings/Default.js");
                    });
            });
        }
    }
}

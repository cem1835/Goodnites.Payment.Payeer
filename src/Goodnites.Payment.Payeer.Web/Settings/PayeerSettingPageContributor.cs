using System.Threading.Tasks;
using Goodnites.Payment.Payeer.Localization;
using Goodnites.Payment.Payeer.Permissions;
using Goodnites.Payment.Payeer.Web.Pages.Components.PayeerSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.SettingManagement.Web.Pages.SettingManagement;

namespace Goodnites.Payment.Payeer.Web.Settings
{
    public class PayeerSettingPageContributor:ISettingPageContributor
    {
        public Task ConfigureAsync(SettingPageCreationContext context)
        {
            var localizer = context.ServiceProvider.GetRequiredService<IStringLocalizer<PayeerResource>>();
            
             context.Groups.Add(new SettingPageGroup("PayeerSettings", localizer["Payeer Settings"], typeof(PayeerSettingsViewComponent)));

            return Task.CompletedTask;
        }

        public async Task<bool> CheckPermissionsAsync(SettingPageCreationContext context)
        {
            var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();
            return await authorizationService.IsGrantedAsync(PayeerPermissions.ApiSetting);
        }
    }
}
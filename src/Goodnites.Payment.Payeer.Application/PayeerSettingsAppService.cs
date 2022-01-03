using System.Threading.Tasks;
using Goodnites.Payment.Payeer.Permissions;
using Goodnites.Payment.Payeer.Settings;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using Volo.Abp.SettingManagement;

namespace Goodnites.Payment.Payeer
{
    [Authorize(PayeerPermissions.ApiSetting)]
    public class PayeerSettingsAppService:PayeerAppService,IPayeerSettingsAppService
    {
        protected readonly ISettingManager SettingManager;

        public PayeerSettingsAppService(ISettingManager settingManager)
        {
            SettingManager = settingManager;
        }
        
        public virtual async Task<PayeerSettingsDto> GetAsync()
        {
            var settingsDto = new PayeerSettingsDto()
            {
                MerchantId = await SettingProvider.GetOrNullAsync(PayeerSettings.PayeerMerchantId),
                SecretKey = await SettingProvider.GetOrNullAsync(PayeerSettings.PayeerSecretKey),
                Min = await SettingProvider.GetOrNullAsync(PayeerSettings.PayeerMin),
                Max = await SettingProvider.GetOrNullAsync(PayeerSettings.PayeerMax),
            };

            if (CurrentTenant.IsAvailable)
            {
                settingsDto.MerchantId =
                    await SettingManager.GetOrNullForTenantAsync(PayeerSettings.PayeerMerchantId,
                        CurrentTenant.GetId(),
                        false);

                settingsDto.SecretKey = await SettingManager.GetOrNullForTenantAsync(
                    PayeerSettings.PayeerSecretKey,
                    CurrentTenant.GetId(), false);
                
                settingsDto.Min = await SettingManager.GetOrNullForTenantAsync(
                    PayeerSettings.PayeerMin,
                    CurrentTenant.GetId(), false);
                
                settingsDto.Max = await SettingManager.GetOrNullForTenantAsync(
                    PayeerSettings.PayeerMax,
                    CurrentTenant.GetId(), false);
            }
            return settingsDto;
        }

        public virtual async Task UpdateAsync(PayeerSettingsDto input)
        {
            if (CurrentTenant.Id.HasValue)
            {
                await SettingManager.SetForTenantAsync(CurrentTenant.Id.Value, PayeerSettings.PayeerMerchantId,
                    input.MerchantId);
                
                await SettingManager.SetForTenantAsync(CurrentTenant.Id.Value, PayeerSettings.PayeerSecretKey,
                    input.SecretKey);
                
                await SettingManager.SetForTenantAsync(CurrentTenant.Id.Value, PayeerSettings.PayeerMin,
                    input.Min);
                
                await SettingManager.SetForTenantAsync(CurrentTenant.Id.Value, PayeerSettings.PayeerMax,
                    input.Max);
            }
            else
            {
                await SettingManager.SetGlobalAsync(PayeerSettings.PayeerMerchantId, input.MerchantId);
                await SettingManager.SetGlobalAsync(PayeerSettings.PayeerSecretKey, input.SecretKey);
                await SettingManager.SetGlobalAsync(PayeerSettings.PayeerMin, input.Min);
                await SettingManager.SetGlobalAsync(PayeerSettings.PayeerMax, input.Max);
            }
        }
    }
}
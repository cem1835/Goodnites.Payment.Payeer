using Volo.Abp.Settings;

namespace Goodnites.Payment.Payeer.Settings
{
    public class PayeerSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(new SettingDefinition(PayeerSettings.PayeerMerchantId,isEncrypted:true));
            context.Add(new SettingDefinition(PayeerSettings.PayeerSecretKey,isEncrypted:true));
            context.Add(new SettingDefinition(PayeerSettings.PayeerMin,defaultValue:"1"));
            context.Add(new SettingDefinition(PayeerSettings.PayeerMax,defaultValue:"10000"));
        }
    }
}
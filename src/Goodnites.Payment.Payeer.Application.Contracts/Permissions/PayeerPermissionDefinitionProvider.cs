using Goodnites.Payment.Payeer.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Goodnites.Payment.Payeer.Permissions
{
    public class PayeerPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var payeerGroup = context.AddGroup(PayeerPermissions.GroupName, L("Permission:Payeer"));

            payeerGroup.AddPermission(PayeerPermissions.ApiSetting, L("PayeerSettings"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<PayeerResource>(name);
        }
    }
}
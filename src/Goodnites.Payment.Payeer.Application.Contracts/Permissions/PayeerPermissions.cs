using Volo.Abp.Reflection;

namespace Goodnites.Payment.Payeer.Permissions
{
    public class PayeerPermissions
    {
        public const string GroupName = "Payeer";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(PayeerPermissions));
        }
        
        public const string ApiSetting = "PayeerSettingsAuth";
    }
}
using Goodnites.Payment.Payeer.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Goodnites.Payment.Payeer.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class PayeerPageModel : AbpPageModel
    {
        protected PayeerPageModel()
        {
            LocalizationResourceType = typeof(PayeerResource);
            ObjectMapperContext = typeof(PayeerWebModule);
        }
    }
}
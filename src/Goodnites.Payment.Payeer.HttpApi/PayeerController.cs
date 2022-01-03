using Goodnites.Payment.Payeer.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Goodnites.Payment.Payeer
{
    public abstract class PayeerController : AbpController
    {
        protected PayeerController()
        {
            LocalizationResource = typeof(PayeerResource);
        }
    }
}

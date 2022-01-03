using Goodnites.Payment.Payeer.Localization;
using Volo.Abp.Application.Services;

namespace Goodnites.Payment.Payeer
{
    public abstract class PayeerAppService : ApplicationService
    {
        protected PayeerAppService()
        {
            LocalizationResource = typeof(PayeerResource);
            ObjectMapperContext = typeof(PayeerApplicationModule);
        }
    }
}

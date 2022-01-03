using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Goodnites.Payment.Payeer.Web.Pages.Components.PayeerSettings
{
    public class PayeerSettingsViewComponent:AbpViewComponent
    {
        private readonly IPayeerSettingsAppService _payeerSettingsAppService;

        public PayeerSettingsViewComponent(IPayeerSettingsAppService payeerSettingsAppService)
        {
            _payeerSettingsAppService = payeerSettingsAppService;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _payeerSettingsAppService.GetAsync();
            
            return View("~/Pages/Components/PayeerSettings/Default.cshtml",model);
        }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Goodnites.Payment.Payeer.Web.Pages.Payeer
{
    public class Success : PageModel
    {
        private readonly IPayeerApiAppService _payeerApiAppService;

        public Success(IPayeerApiAppService payeerApiAppService)
        {
            _payeerApiAppService = payeerApiAppService;
        }

        public async Task OnGetAsync(PayeerResponseModel model)
        {
            var clientIp = HttpContext.Connection.RemoteIpAddress.ToString();

            await _payeerApiAppService.WebHookAsync(model, clientIp);
        }
        
        
    }
}
using System.Threading.Tasks;
using Goodnites.Payment.Payeer.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace Goodnites.Payment.Payeer
{
    [RemoteService(Name = "GoodnitesPayeerSetting")]
    [Microsoft.AspNetCore.Components.Route("/api/payeerSetting")]
    [Authorize(PayeerPermissions.ApiSetting)]
    public class PayeerSettingsController:PayeerController,IPayeerSettingsAppService
    {
        private readonly IPayeerSettingsAppService _payeerSettingsAppService;

        public PayeerSettingsController(IPayeerSettingsAppService payeerSettingsAppService)
        {
            _payeerSettingsAppService = payeerSettingsAppService;
        }

        [HttpGet]
        public Task<PayeerSettingsDto> GetAsync()
        {
            return _payeerSettingsAppService.GetAsync();
        }

        [HttpPut]
        public async Task UpdateAsync(PayeerSettingsDto input)
        {
            await _payeerSettingsAppService.UpdateAsync(input);
        }
    }
}
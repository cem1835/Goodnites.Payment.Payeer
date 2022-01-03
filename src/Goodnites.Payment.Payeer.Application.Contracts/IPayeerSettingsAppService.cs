using System.Threading.Tasks;

namespace Goodnites.Payment.Payeer
{
    public interface IPayeerSettingsAppService
    {
        Task<PayeerSettingsDto> GetAsync();

        Task UpdateAsync(PayeerSettingsDto input);
    }
}
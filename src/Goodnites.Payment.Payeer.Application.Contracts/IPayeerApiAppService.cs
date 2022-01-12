using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Goodnites.Payment.Payeer
{
    public interface IPayeerApiAppService:ITransientDependency
    {
        Task WebHookAsync(PayeerResponseModel payeerResponseModel, string clientIp);

        Task<string> CreateChargeUrlAsync(string amount, string currency, string description);
    }
}
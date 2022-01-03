using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Goodnites.Payment.Payeer.Samples
{
    public interface ISampleAppService : IApplicationService
    {
        Task<SampleDto> GetAsync();

        Task<SampleDto> GetAuthorizedAsync();
    }
}

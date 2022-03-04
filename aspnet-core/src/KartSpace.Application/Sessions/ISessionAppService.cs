using System.Threading.Tasks;
using Abp.Application.Services;
using KartSpace.Sessions.Dto;

namespace KartSpace.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}

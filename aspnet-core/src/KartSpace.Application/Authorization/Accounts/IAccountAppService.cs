using System.Threading.Tasks;
using Abp.Application.Services;
using KartSpace.Authorization.Accounts.Dto;

namespace KartSpace.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}

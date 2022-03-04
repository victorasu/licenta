using Abp.Application.Services;
using KartSpace.MultiTenancy.Dto;

namespace KartSpace.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}


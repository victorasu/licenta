using Abp.Application.Services;
using Abp.Application.Services.Dto;
using KartSpace.Merchandise.Dto;

namespace KartSpace.Merchandise
{
    public interface IMerchAppService : IAsyncCrudAppService<MerchDto, int, PagedMerchResultRequestDto, MerchDto, MerchDto>
    {
        
    }
}
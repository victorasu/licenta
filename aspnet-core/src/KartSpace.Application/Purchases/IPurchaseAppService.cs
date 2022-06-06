using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using KartSpace.Purchases.Dto;

namespace KartSpace.Purchases
{
    public interface IPurchaseAppService : IAsyncCrudAppService<PurchaseDto, int, PagedPurchaseResultRequestDto, PurchaseDto, PurchaseDto, EntityDto<int>, EntityDto<int>>
    {
        Task BuyMerchAsync(BuyMerchDto input);
        Task EditStateAsync(EditStateDto input);
        Task<PagedResultDto<PurchaseResultDto>> DisplayPurchases(PagedPurchaseResultRequestDto input, TipStareComanda category);
    }
}
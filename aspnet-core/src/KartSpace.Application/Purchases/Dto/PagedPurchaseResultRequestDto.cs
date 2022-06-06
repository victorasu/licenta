using Abp.Application.Services.Dto;

namespace KartSpace.Purchases.Dto
{
    public class PagedPurchaseResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
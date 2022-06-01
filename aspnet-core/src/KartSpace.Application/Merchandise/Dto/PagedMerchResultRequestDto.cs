using Abp.Application.Services.Dto;

namespace KartSpace.Merchandise.Dto
{
    public class PagedMerchResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
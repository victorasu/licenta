using Abp.Application.Services.Dto;

namespace KartSpace.Events.Dto;

public class PagedEventResultRequestDto : PagedResultRequestDto
{
    public string Keyword { get; set; }
}
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using KartSpace.Events.Dto;
using System.Threading.Tasks;

namespace KartSpace.Events;

public interface IEventAppService : IAsyncCrudAppService<EventDto, int, PagedEventResultRequestDto, EventDto, EventDto, EntityDto<int>, EntityDto<int>>
{
    public string GetDisplayName(TipEveniment enumValue);
    public Task<PagedResultDto<EventResultDto>> GetEventsList(PagedEventResultRequestDto input, TipEveniment category);
}
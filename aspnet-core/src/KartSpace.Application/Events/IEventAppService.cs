using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using KartSpace.Events.Dto;

namespace KartSpace.Events;

public interface IEventAppService : IAsyncCrudAppService<EventDto, int, PagedEventResultRequestDto, EventDto, EventDto, EntityDto<int>, EntityDto<int>>
{
    
}
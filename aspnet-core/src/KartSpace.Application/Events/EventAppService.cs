using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using KartSpace.Events.Dto;

namespace KartSpace.Events;

public class EventAppService : AsyncCrudAppService<Event, EventDto, int, PagedEventResultRequestDto, EventDto, EventDto, EntityDto<int>, EntityDto<int>>, IEventAppService
{
    private readonly IRepository<Event, int> _eventRepository;

    public EventAppService(
        IRepository<Event, int> eventRepository)
        : base(eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public override async Task<EventDto> CreateAsync(EventDto input)
    {
        var theEvent = ObjectMapper.Map<Event>(input);

        await _eventRepository.InsertAsync(theEvent);

        return ObjectMapper.Map<EventDto>(theEvent);
    }

    public override async Task<EventDto> UpdateAsync(EventDto input)
    {
        var theEvent = ObjectMapper.Map<Event>(input);

        await _eventRepository.UpdateAsync(theEvent);

        return ObjectMapper.Map<EventDto>(theEvent);
    }

    protected override IQueryable<Event> CreateFilteredQuery(PagedEventResultRequestDto input)
    {
        return _eventRepository.GetAll()
            .WhereIf(!input.Keyword.IsNullOrWhiteSpace(),
                x => x.Title.Contains(input.Keyword)
                     || x.Description.Contains(input.Keyword)
                     || x.StartTime.ToString().Contains(input.Keyword)
                     || x.EndTime.HasValue.ToString().Contains(input.Keyword));
    }
}
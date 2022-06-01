using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using KartSpace.Events.Dto;

namespace KartSpace.Events;

/// <summary>
/// Application service for the events section
/// </summary>
public class EventAppService : AsyncCrudAppService<Event, EventDto, int, PagedEventResultRequestDto, EventDto, EventDto, EntityDto<int>, EntityDto<int>>, IEventAppService
{
    private readonly IRepository<Event, int> _eventRepository;

    public EventAppService(
        IRepository<Event, int> eventRepository)
        : base(eventRepository)
    {
        _eventRepository = eventRepository;
    }

    /// <summary>
    /// Asynchronous method for creating an Event entity and inserting it into the database
    /// </summary>
    /// <param name="input">Event DTO data, does not contain Id</param>
    /// <returns>EventDto of the inserted Event</returns>
    public override async Task<EventDto> CreateAsync(EventDto input)
    {
        var theEvent = ObjectMapper.Map<Event>(input);

        await _eventRepository.InsertAsync(theEvent);

        return ObjectMapper.Map<EventDto>(theEvent);
    }

    /// <summary>
    /// Asynchronous method for updating an Event entity's data in the database
    /// </summary>
    /// <param name="input">Event DTO data, contains Id</param>
    /// <returns>EventDto of the updated Event</returns>
    public override async Task<EventDto> UpdateAsync(EventDto input)
    {
        var theEvent = ObjectMapper.Map<Event>(input);

        await _eventRepository.UpdateAsync(theEvent);

        return ObjectMapper.Map<EventDto>(theEvent);
    }

    /// <summary>
    /// Filters the database records of Events by using pagination data
    /// </summary>
    /// <param name="input">Filter keyword, pagination and skip count</param>
    /// <returns>IQueryable of filtered Events</returns>
    protected override IQueryable<Event> CreateFilteredQuery(PagedEventResultRequestDto input)
    {
        var events = _eventRepository.GetAll()
            .WhereIf(!input.Keyword.IsNullOrWhiteSpace(),
                x => x.Title.Contains(input.Keyword)
                     || x.Description.Contains(input.Keyword)
                     || x.StartTime.ToString().Contains(input.Keyword)
                     || x.EndTime.HasValue.ToString().Contains(input.Keyword));
        return events;
    }

    /// <summary>
    /// Filters events by input filter and selected category
    /// </summary>
    /// <param name="input">Filter keyword, pagination and skip count</param>
    /// <param name="category">Category type provided in the UI dropdown</param>
    /// <returns>List of Event objects</returns>
    public async Task<PagedResultDto<EventResultDto>> GetEventsList(PagedEventResultRequestDto input, TipEveniment category)
    {
        var events = CreateFilteredQuery(input);

        var query = from eveniment in events
                    where (category.Equals(TipEveniment.Alege) ? true : eveniment.Category.Equals(category))
                    select eveniment;

        var queryRez = await AsyncQueryableExecuter.ToListAsync(query);

        var eventList = queryRez.Select(x =>
        {
            var lista = ObjectMapper.Map<EventResultDto>(x);
            lista.CategoryName = GetDisplayName(lista.Category);
            return lista;
        }).ToList();

        var eventDisplay = new PagedResultDto<EventResultDto>(eventList.Count, eventList);

        return eventDisplay;
    }

    /// <summary>
    /// Gets <see cref="TipEveniment"/> value's DisplayName
    /// </summary>
    /// <param name="enumValue">Value of selected Enum option</param>
    /// <returns>Selected Enum option's DisplayName</returns>
    public string GetDisplayName(TipEveniment enumValue)
    {
        string displayName;

        displayName = enumValue.GetType()
            .GetMember(enumValue.ToString())
            .FirstOrDefault()
            .GetCustomAttribute<DisplayAttribute>()?
            .GetName();

        if (String.IsNullOrEmpty(displayName))
        {
            displayName = enumValue.ToString();
        }

        return displayName;
    }
}
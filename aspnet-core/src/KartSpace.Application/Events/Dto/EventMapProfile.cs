﻿using AutoMapper;

namespace KartSpace.Events.Dto;

public class EventMapProfile : Profile
{
    public EventMapProfile()
    {
        CreateMap<EventDto, Event>();
        CreateMap<Event, EventDto>();
        CreateMap<EventResultDto, Event>();
        CreateMap<Event, EventResultDto>();
    }
}
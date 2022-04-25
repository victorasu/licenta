using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Timing;

namespace KartSpace.Events.Dto;

public class EventDto : EntityDto<int>
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public TipEveniment Category { get; set; }

    [Required]
    public DateTime StartTime { get; set; } = Clock.Now;

    public DateTime? EndTime { get; set; }
}
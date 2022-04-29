using Abp.Application.Services.Dto;
using Abp.Timing;
using System;
using System.ComponentModel.DataAnnotations;

namespace KartSpace.Events.Dto
{
    public class EventResultDto : EntityDto<int>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public TipEveniment Category { get; set; }

        public string CategoryName { get; set; }

        [Required]
        public DateTime StartTime { get; set; } = Clock.Now;

        public DateTime? EndTime { get; set; }
    }
}

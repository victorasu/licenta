using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using KartSpace.Events;

namespace KartSpace.Merchandise.Dto
{
    [AutoMapFrom(typeof(Merch))]
    public class MerchDto : EntityDto<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public TipMerch Category { get; set; }
        [Required] 
        public double Price { get; set; }
    }
}
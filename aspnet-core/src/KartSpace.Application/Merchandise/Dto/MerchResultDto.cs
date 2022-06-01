using Abp.Application.Services.Dto;

namespace KartSpace.Merchandise.Dto
{
    public class MerchResultDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TipMerch Category { get; set; }
        public string CategoryName { get; set; }
        public double Price { get; set; }
        public int TenantId { get; set; }
    }
}
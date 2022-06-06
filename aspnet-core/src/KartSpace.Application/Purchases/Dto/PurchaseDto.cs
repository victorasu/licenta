using System;
using Abp.Application.Services.Dto;

namespace KartSpace.Purchases.Dto
{
    public class PurchaseDto : EntityDto<int>
    {
        public int MerchId { get; set; }
        public long UserId { get; set; }
        public DateTime CreationDateTime { get; set; }
        public TipStareComanda StareComanda { get; set; }
    }
}
using System;
using Abp.Application.Services.Dto;

namespace KartSpace.Purchases.Dto
{
    public class PurchaseResultDto : EntityDto<int>
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string UserFullName { get; set; }
        public string UserPhoneNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationDateString { get; set; }
        public TipStareComanda TipStareComanda { get; set; }
        public string StareComandaName { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace KartSpace.Purchases
{
    [Table("Purchases")]
    public class Purchase : Entity<int>
    {
        public int MerchId { get; set; }
        public long UserId { get; set; }
        public DateTime CreationDateTime { get; set; }
        public TipStareComanda StareComanda { get; set; }
    }
}
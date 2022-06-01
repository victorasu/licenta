using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace KartSpace.Merchandise
{
    [Table("Merchandise")]
    public class Merch : Entity<int>, IMustHaveTenant
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public TipMerch Category { get; set; }
        [Required]
        public double Price { get; set; }
        public int TenantId { get; set; }
    }
}
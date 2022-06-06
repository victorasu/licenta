using System.ComponentModel.DataAnnotations;

namespace KartSpace.Purchases
{
    /// <summary>
    /// Starea comenzii
    /// </summary>
    public enum TipStareComanda
    {
        [Display(Name = "Plasată")]
        Plasata = 0,
        [Display(Name = "Confirmată")]
        Confirmata = 1,
        [Display(Name = "Trimisă")]
        Trimisa = 2,
        [Display(Name = "Completă")]
        Completa = 3
    }
}
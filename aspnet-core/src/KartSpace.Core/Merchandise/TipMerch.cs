using System.ComponentModel.DataAnnotations;

namespace KartSpace.Merchandise
{
    /// <summary>
    /// Tipul produsului
    /// </summary>
    public enum TipMerch
    {
        [Display(Name = "Alege")]
        Alege = 0,
        [Display(Name = "Kart")]
        Kart = 1,
        [Display(Name = "Echipament protectie")]
        EchipamentProtectie = 2,
        [Display(Name = "Piese schimb")]
        PieseSchimb = 3,
        [Display(Name = "Accesorii")]
        Accesorii = 4
    }
}
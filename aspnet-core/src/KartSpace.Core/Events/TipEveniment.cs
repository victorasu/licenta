using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KartSpace.Events
{
    /// <summary>
    /// Tipul evenimentului
    /// </summary>
    public enum TipEveniment
    {
        [Display(Name = "Alege tipul evenimentului")]
        Alege = 0,
        [Display(Name = "Campionatul national")]
        CampionatulNational = 1,
        [Display(Name = "Serii speciale")]
        SerieSpeciala = 2,
        [Display(Name = "Cupa romaniei")]
        CupaRomaniei = 3,
        [Display(Name = "Campionatul international")]
        CampionatulInternational = 4,
        [Display(Name = "Gala campionilor")]
        GalaCampionilor = 5,
    }
}

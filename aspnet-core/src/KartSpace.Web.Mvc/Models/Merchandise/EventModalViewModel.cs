using KartSpace.Merchandise.Dto;

namespace KartSpace.Web.Models.Merchandise
{
    public class MerchModalViewModel
    {
        public MerchDto Merch { get; set; } = new MerchDto();
        public string PhoneNumber { get; set; }
    }
}

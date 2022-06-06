using AutoMapper;

namespace KartSpace.Purchases.Dto
{
    public class PurchaseMapProfile : Profile
    {
        public PurchaseMapProfile()
        {
            CreateMap<Purchase, PurchaseDto>();
            CreateMap<PurchaseDto, Purchase>();
        }
    }
}
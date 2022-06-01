using System.Collections.Generic;
using Abp.Application.Services.Dto;
using AutoMapper;

namespace KartSpace.Merchandise.Dto
{
    public class MerchMapProfile : Profile
    {
        public MerchMapProfile()
        {
            CreateMap<Merch, MerchDto>();
            CreateMap<MerchDto, Merch>();
            CreateMap<MerchResultDto, Merch>();
            CreateMap<Merch, MerchResultDto>();
        }
    }
}
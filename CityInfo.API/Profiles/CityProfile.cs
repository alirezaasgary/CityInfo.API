using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Models_Entity;

namespace CityInfo.API.Profiles
{
    public class CityProfile:Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityWithOutPointOfIntrestDto>()
            .ForMember(dest => dest.Descriptions, opt => opt.MapFrom(src => src.Description));


            CreateMap<City,CityDto>();
        }
    }
}

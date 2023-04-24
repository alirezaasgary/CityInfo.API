using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Models_Entity;

namespace CityInfo.API.Profiles
{
    public class PointOfIntrestProfile:Profile
    {
        public PointOfIntrestProfile()
        {
            CreateMap<PointOfIntrest,PointOfIntrestDto>()
             .ForMember(dest => dest.cityName, opt => opt.MapFrom(src => src.City.Name ))
             .ForMember(dest => dest.Institution, opt => opt.MapFrom(src => src.City.Affiliation.Name))


             ;

            CreateMap<PointOfIntrest, PointOfIntrestForUpdateDto>();

            CreateMap<PointOfIntrestForCreateDto, PointOfIntrest>(); //برای کریت کردن باید ویو مدل هارو توی جداول یا انتیتی های مپ کنیم

            CreateMap<PointOfIntrestForUpdateDto, PointOfIntrest>();
        }
    }
}

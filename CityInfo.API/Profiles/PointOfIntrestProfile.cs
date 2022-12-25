using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Models_Entity;

namespace CityInfo.API.Profiles
{
    public class PointOfIntrestProfile:Profile
    {
        public PointOfIntrestProfile()
        {
            CreateMap<PointOfIntrest,PointOfIntrestDto>();

            CreateMap<PointOfIntrest, PointOfIntrestForUpdateDto>();

            CreateMap<PointOfIntrestForCreateDto, PointOfIntrest>(); //برای کریت کردن باید ویو مدل هارو توی جداول یا انتیتی های مپ کنیم

            CreateMap<PointOfIntrestForUpdateDto, PointOfIntrest>();
        }
    }
}

using CityInfo.API.Models;
using CityInfo.API.Repotories;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/Cities")]
    public class CitiesController : ControllerBase
    {
        //private readonly CityDataStore cityDataStore;
        private readonly ICityInfoRepository _cityInfoRepository;

        public CitiesController(/* CityDataStore cityDataStore*/ ICityInfoRepository cityInfoRepository)
        {
            //this.cityDataStore = cityDataStore;
            _cityInfoRepository = cityInfoRepository 
                ?? throw new ArgumentNullException(nameof(cityInfoRepository)); ///پرتاب خطا در صورت تنظیم نبودن اینترفیس و سرویس
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithOutPointOfIntrestDto>>> GetCities()
        {
            var cities = await _cityInfoRepository.GetCitiesAsync();

            var result = new List<CityWithOutPointOfIntrestDto>();
            foreach (var city in cities)
            {
                result.Add(new CityWithOutPointOfIntrestDto()
                {
                    Description = city.Description,
                    Id = city.Id,
                    Name = city.Name
                });
                    
            }

            return Ok(result);


        }
        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            return Ok();

        }
    }
}

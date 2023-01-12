using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/Cities")]
    public class CitiesController : ControllerBase
    {
        //private readonly CityDataStore cityDataStore;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(/* CityDataStore cityDataStore*/ ICityInfoRepository cityInfoRepository,IMapper mapper)
        {
            //this.cityDataStore = cityDataStore;
            _cityInfoRepository = cityInfoRepository 
                ?? throw new ArgumentNullException(nameof(cityInfoRepository)); ///پرتاب خطا در صورت تنظیم نبودن اینترفیس و سرویس

            _mapper = mapper 
                ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithOutPointOfIntrestDto>>> GetCities()
        {
            var cities = await _cityInfoRepository.GetCitiesAsync();

            //var result = new List<CityWithOutPointOfIntrestDto>();
            //foreach (var city in cities)
            //{
            //    result.Add(new CityWithOutPointOfIntrestDto()
            //    {
            //        Description = city.Description,
            //        Id = city.Id,
            //        Name = city.Name
            //    });
                    
            //} //به دلیل افزودن  اتو مپر دیگر نیازی به  نمونه سازی نیست.


            return Ok(_mapper.Map<IEnumerable<CityWithOutPointOfIntrestDto>>(cities));


        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id,bool pointInterest=false) // Change ActionResult -> IActionResult
         {
            var city = await  _cityInfoRepository.GetCityAsync(id, pointInterest);

            if(city == null)
            {
                return NotFound();
            }

            if (!pointInterest)
            {
                return Ok(_mapper.Map<CityWithOutPointOfIntrestDto>(city)); //pointInterest=false
            }
            else
            {
                return Ok(_mapper.Map<CityDto>(city)); //pointInterest=true 
            }

            

        }
    }
}

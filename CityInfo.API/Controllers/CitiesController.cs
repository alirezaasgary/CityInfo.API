using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/Cities")]
    public class CitiesController : ControllerBase
    {
        private readonly CityDataStore cityDataStore;

        public CitiesController( CityDataStore cityDataStore)
        {
            this.cityDataStore = cityDataStore;
        }
        [HttpGet]
        public ActionResult<CityDto> GetCities()
        {
            var cities = cityDataStore.Cities;


            return Ok(cities);


        }
        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            var city = cityDataStore.Cities.FirstOrDefault(x => x.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(city);
            }

        }
    }
}

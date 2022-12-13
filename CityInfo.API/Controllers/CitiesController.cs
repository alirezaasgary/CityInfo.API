using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/Cities")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<CityDto> GetCities()
        {
            var cities = CityDataStore.current.Cities;


            return Ok(cities);


        }
        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            var city = CityDataStore.current.Cities.FirstOrDefault(x => x.Id == id);
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

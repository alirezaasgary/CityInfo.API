using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/Cities")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetCities()
        {

            return new JsonResult(CityDataStore.current.Cities);

        }
        [HttpGet("{id}")]
        public JsonResult GetCity(int id)
        {
            return new JsonResult(CityDataStore.current.Cities.FirstOrDefault(x => x.Id == id));
        }
    }
}

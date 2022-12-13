using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    public class CitiesController : ControllerBase
    {
        [HttpGet("api/cities")]
        public JsonResult GetCities()
        {

            return new JsonResult(
                  new List<object>
                  {
                    new {id=1,name="tehran" },
                    new {id=2,name="Moscow"}
                  }
                  );

        }
    }
}

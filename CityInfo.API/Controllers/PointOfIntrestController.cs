using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{id}/pointofintrest")]
    [ApiController]
    public class PointOfIntrestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfIntrestDto>> GetPointOfIntrests(int id)
        {
            var city = CityDataStore.current.Cities.FirstOrDefault(c => c.Id == id);    

            if (city == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(city.PointOfIntrests);
            }
        }

        [HttpGet("{pointId}")]
        public ActionResult<PointOfIntrestDto> GetPointOfIntrest(int id,int pointId)
        {
            var city = CityDataStore.current.Cities.FirstOrDefault(c => c.Id == id);
            if(city == null)
            {
                return NotFound();
            }
            var point = city.PointOfIntrests.FirstOrDefault(p => p.Id == pointId);
            if (point == null)
            {
                return NotFound();
            }
            
                
                return Ok(point);
            
        }
    }
}

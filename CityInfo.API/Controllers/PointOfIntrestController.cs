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

        [HttpGet("{pointId}",Name = "GetPointOfIntrest")]
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

        [HttpPost]
        public ActionResult<PointOfIntrestDto> CreatepointOfIntrest
            (int id,
            PointOfIntrestForCreateDto pointOfIntrest)//اتربیوت فرام بادی به صورت دیفالت هم همین است یعنی این این امکان وجود دارد که این اتربوت رانزاریم.
        {
            var city= CityDataStore.current.Cities.FirstOrDefault(p => p.Id == id);
            if(city == null)
            {
                return NotFound();  
            }
            var maxPointOfIntrestId = CityDataStore.current.Cities.
                SelectMany(p => p.PointOfIntrests).
                Max(p => p.Id);

            var point = new PointOfIntrestDto()
            {
                Id = ++maxPointOfIntrestId,
                Name = pointOfIntrest.Name,
                Description = pointOfIntrest.Description
            };

            city.PointOfIntrests.Add(point);

            return CreatedAtAction("GetPointOfIntrest", new
            {
                id = id,
                pointId=point.Id
            },
            point
            
            );    
        }
    }
}

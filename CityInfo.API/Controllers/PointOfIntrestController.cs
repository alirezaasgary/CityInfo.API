using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointofintrest")]
    [ApiController]
    public class PointOfIntrestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfIntrestDto>> GetPointOfIntrests(int cityId)
        {
            var city = CityDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(city.PointOfIntrests);
            }
        }

        [HttpGet("{pointId}", Name = "GetPointOfIntrest")]
        public ActionResult<PointOfIntrestDto> GetPointOfIntrest(int cityId, int pointId)
        {
            var city = CityDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
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
        #region Create
        [HttpPost]
        public ActionResult<PointOfIntrestDto> CreatepointOfIntrest
    (int cityId,
    PointOfIntrestForCreateDto pointOfIntrest)//اتربیوت فرام بادی به صورت دیفالت هم همین است یعنی این این امکان وجود دارد که این اتربوت رانزاریم.
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var city = CityDataStore.current.Cities.FirstOrDefault(p => p.Id == cityId);
            if (city == null)
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
                cityId = cityId,
                pointId = point.Id
            },
            point

            );
        }

        #endregion

        #region Update

        [HttpPut("{pointId}")]
        public ActionResult UpdatePointOfIntrestDto(
            int cityId,
            int pointId,
            PointOfIntrestForUpdateDto pointOfIntrest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var city = CityDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();

            var point = city.PointOfIntrests.FirstOrDefault(p => p.Id == pointId);
            if (point == null)
                return NotFound();

            point.Name = pointOfIntrest.Name;
            point.Description = pointOfIntrest.Description;

            return NoContent();
        }

        #endregion

        #region Update with patch
        [HttpPatch("{pointId}")]
        public ActionResult PartiallyUpdatePointOfIntrest(
            int cityId,
            int pointId,
            JsonPatchDocument<PointOfIntrestForUpdateDto> patchDocument 

            )
        {
            

            var city = CityDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();

            var point = city.PointOfIntrests.FirstOrDefault(p => p.Id == pointId);
            if (point == null)
                return NotFound();

            var pointOfIntrestToPath = new PointOfIntrestForUpdateDto
            {
                Name = point.Name,
                Description = point.Description
            };

            patchDocument.ApplyTo(pointOfIntrestToPath,ModelState);

           

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            point.Name = pointOfIntrestToPath.Name;
            point.Description = pointOfIntrestToPath.Description;

            return NoContent();
        }

        #endregion
    }
}

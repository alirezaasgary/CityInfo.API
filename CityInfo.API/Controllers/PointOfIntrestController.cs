﻿using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointofintrest")]
    [ApiController]
    public class PointOfIntrestController : ControllerBase
    {
        private readonly ILogger<PointOfIntrestController> _logger;
        private readonly ILocalMailService _localMailService;
        private readonly CityDataStore cityDataStore;

        public PointOfIntrestController(ILogger<PointOfIntrestController> logger, ILocalMailService localMailService, CityDataStore cityDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localMailService = localMailService ?? throw new ArgumentNullException(nameof(localMailService));
            this.cityDataStore = cityDataStore;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointOfIntrestDto>> GetPointOfIntrests(int cityId)
        {
            var city = cityDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            try
            {
                //throw new Exception("create error sample"); //ایجاد اکسپشن دستی جهت تست لاگ اررور اکشن
                if (city == null)
                {
                    _logger.LogInformation($"city with id={cityId} was not found");
                    return NotFound();
                }
                else
                {
                    return Ok(city.PointOfIntrests);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exeption error for cityId={cityId}", ex);
                return StatusCode(500, "Server has problem");
            }
        }

        [HttpGet("{pointId}", Name = "GetPointOfIntrest")]
        public ActionResult<PointOfIntrestDto> GetPointOfIntrest(int cityId, int pointId)
        {
            var city = cityDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
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

            var city = cityDataStore.Cities.FirstOrDefault(p => p.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var maxPointOfIntrestId = cityDataStore.Cities.
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

            var city = cityDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
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


            var city = cityDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
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

            patchDocument.ApplyTo(pointOfIntrestToPath, ModelState);



            if (!ModelState.IsValid) //این برای مدل استیت اصلی است 
            {
                return BadRequest();
            }
            if (!TryValidateModel(pointOfIntrestToPath))
            {
                return BadRequest();
            }

            point.Name = pointOfIntrestToPath.Name;
            point.Description = pointOfIntrestToPath.Description;

            return NoContent();
        }

        #endregion

        #region Delete
        [HttpDelete("{pointId}")]
        public ActionResult DeletePointOfIntrest(int cityId, int pointId)
        {
            var city = cityDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();

            var point = city.PointOfIntrests.FirstOrDefault(p => p.Id == pointId);
            if (point == null)
                return NotFound();

            city.PointOfIntrests.Remove(point);

            //امکان ارسال ایمسل
            _localMailService.Send("Delete Point of intrest",
                $"point of intrest name={point.Name} was delete"
                );


            return NoContent();

        }
        #endregion
    }
}

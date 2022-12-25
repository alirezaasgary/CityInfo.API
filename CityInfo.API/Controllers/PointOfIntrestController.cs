using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Models_Entity;
using CityInfo.API.Repository;
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
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointOfIntrestController(ILogger<PointOfIntrestController> logger, ILocalMailService localMailService, CityDataStore cityDataStore
           ,ICityInfoRepository cityInfoRepository, IMapper mapper
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localMailService = localMailService ?? throw new ArgumentNullException(nameof(localMailService));
            this.cityDataStore = cityDataStore;
            _cityInfoRepository = cityInfoRepository
               ?? throw new ArgumentNullException(nameof(cityInfoRepository)); ///پرتاب خطا در صورت تنظیم نبودن اینترفیس و سرویس

            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));


        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfIntrestDto>>> GetPointOfIntrests(int cityId)
        {
             var point = await _cityInfoRepository.GetPointsAsync(cityId);

            if(!await _cityInfoRepository.CityExistAsync(cityId))
            {
                _logger.LogInformation($"city with id={cityId} was not found");
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<PointOfIntrestDto>>(point));

            //var city = cityDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            //try
            //{
            //    //throw new Exception("create error sample"); //ایجاد اکسپشن دستی جهت تست لاگ اررور اکشن
            //    if (city == null)
            //    {
            //        _logger.LogInformation($"city with id={cityId} was not found");
            //        return NotFound();
            //    }
            //    else
            //    {
            //        return Ok(city.PointOfIntrests);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogCritical($"Exeption error for cityId={cityId}", ex);
            //    return StatusCode(500, "Server has problem");
            //}
        }

        [HttpGet("{pointId}", Name = "GetPointOfIntrest")]
        public async Task<ActionResult<PointOfIntrestDto>> GetPointOfIntrest(int cityId, int pointId)
        {


            if(! await _cityInfoRepository.CityExistAsync(cityId))
            {
                _logger.LogInformation($"city with id={cityId} was not found");
                return NotFound();
            }

            if(!await _cityInfoRepository.PointOfIntrestExistAsync(cityId, pointId))
            {
                _logger.LogInformation($"city with cityId={cityId} and pointId={pointId} was not found");
                return NotFound();
            }

            var point = await _cityInfoRepository.GetPointAsync(cityId, pointId);

            return Ok(_mapper.Map<PointOfIntrestDto>(point));

            //var city = cityDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //{
            //    return NotFound();
            //}
            //var point = city.PointOfIntrests.FirstOrDefault(p => p.Id == pointId);
            //if (point == null)
            //{
            //    return NotFound();
            //}


            //return Ok(point);

        }
        #region Create
        [HttpPost]
        public async Task<ActionResult<PointOfIntrestDto>> CreatepointOfIntrest
          (int cityId,
           PointOfIntrestForCreateDto pointOfIntrest)//اتربیوت فرام بادی به صورت دیفالت هم همین است یعنی این این امکان وجود دارد که این اتربوت رانزاریم.
        {
            if (!await _cityInfoRepository.CityExistAsync(cityId))
            {
                _logger.LogInformation($"city with id={cityId} was not found");
                return NotFound();
            }

            var finalpoint = _mapper.Map<PointOfIntrest>(pointOfIntrest);
            await _cityInfoRepository.AddPointOfIntrestAsync(cityId,finalpoint);
            await _cityInfoRepository.SaveChangAsync();

            var createPoint = _mapper.Map<PointOfIntrestDto>(finalpoint);

            return CreatedAtAction("GetPointOfIntrest", new{cityId = cityId,pointId = createPoint.Id}, createPoint);  

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            //var city = cityDataStore.Cities.FirstOrDefault(p => p.Id == cityId);
            //if (city == null)
            //{
            //    return NotFound();
            //}
            //var maxPointOfIntrestId = cityDataStore.Cities.
            //    SelectMany(p => p.PointOfIntrests).
            //    Max(p => p.Id);

            //var point = new PointOfIntrestDto()
            //{
            //    Id = ++maxPointOfIntrestId,
            //    Name = pointOfIntrest.Name,
            //    Description = pointOfIntrest.Description
            //};

            //city.PointOfIntrests.Add(point);

            //return CreatedAtAction("GetPointOfIntrest", new
            //{
            //    cityId = cityId,
            //    pointId = point.Id
            //},
            //point

            //);
        }

        #endregion

        #region Update

        [HttpPut("{pointId}")]
        public async Task<ActionResult> UpdatePointOfIntrestDto(
            int cityId,
            int pointId,
            PointOfIntrestForUpdateDto pointOfIntrest)
        {

             
            if (! await _cityInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }
            if (!await _cityInfoRepository.PointOfIntrestExistAsync(cityId,pointId))
            {
                return NotFound();
            }

            var point = _mapper.Map<PointOfIntrest>(pointOfIntrest);
            point.Id = pointId;
            point.CityId= cityId;
            await _cityInfoRepository.UpdatePointOfIntrestAsync(cityId, point);
            await _cityInfoRepository.SaveChangAsync();

            return NoContent();

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            //var city = cityDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //    return NotFound();

            //var point = city.PointOfIntrests.FirstOrDefault(p => p.Id == pointId);
            //if (point == null)
            //    return NotFound();

            //point.Name = pointOfIntrest.Name;
            //point.Description = pointOfIntrest.Description;

            //return NoContent();
        }

        #endregion

        #region Update with patch
        [HttpPatch("{pointId}")]
        public async Task<ActionResult> PartiallyUpdatePointOfIntrest(
            int cityId,
            int pointId,
            JsonPatchDocument<PointOfIntrestForUpdateDto> patchDocument

            )
        {
            if (!await _cityInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }
            if (!await _cityInfoRepository.PointOfIntrestExistAsync(cityId, pointId))
            {
                return NotFound();
            }
            var point= await _cityInfoRepository.GetPointAsync(cityId,pointId);

            var pointToPatch = _mapper.Map<PointOfIntrestForUpdateDto>(point);

            patchDocument.ApplyTo(pointToPatch, ModelState);

            if (!ModelState.IsValid) //این برای مدل استیت اصلی است 
            {
                return BadRequest();
            }
            if (!TryValidateModel(pointToPatch))
            {
                return BadRequest();
            }
            _mapper.Map(pointToPatch, point);

            await _cityInfoRepository.SaveChangAsync();
            return NoContent();

            //var city = cityDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //    return NotFound();

            //var point = city.PointOfIntrests.FirstOrDefault(p => p.Id == pointId);
            //if (point == null)
            //    return NotFound();

            //var pointOfIntrestToPath = new PointOfIntrestForUpdateDto
            //{
            //    Name = point.Name,
            //    Description = point.Description
            //};

            //patchDocument.ApplyTo(pointOfIntrestToPath, ModelState);



            //if (!ModelState.IsValid) //این برای مدل استیت اصلی است 
            //{
            //    return BadRequest();
            //}
            //if (!TryValidateModel(pointOfIntrestToPath))
            //{
            //    return BadRequest();
            //}

            //point.Name = pointOfIntrestToPath.Name;
            //point.Description = pointOfIntrestToPath.Description;

            //return NoContent();
        }

        #endregion

        #region Delete
        [HttpDelete("{pointId}")]
        public async Task<ActionResult> DeletePointOfIntrest(int cityId, int pointId)
        {

            if (!await _cityInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }
            var point = await _cityInfoRepository.GetPointAsync(cityId,pointId);

            if(point == null)
            {
                return NotFound();
            }
            _cityInfoRepository.DeletePointOfIntrestAsync(point);
            await _cityInfoRepository.SaveChangAsync();


            //var city = cityDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //    return NotFound();

            //var point = city.PointOfIntrests.FirstOrDefault(p => p.Id == pointId);
            //if (point == null)
            //    return NotFound();

            //city.PointOfIntrests.Remove(point);

            //امکان ارسال ایمسل
            _localMailService.Send("Delete Point of intrest",
                $"point of intrest name={point.Name} was delete"
                );


            return NoContent();

        }
        #endregion
    }
}

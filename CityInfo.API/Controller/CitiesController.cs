using System.Collections.Generic;
using System.Linq;
using CityInfo.API.Entities;
using CityInfo.API.Model;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controller
{
    [Route("api/cities")]
    public class CitiesController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();
            var results = new List<CityDetails>();
            if (cityEntities == null) return new JsonResult("No Data Found");

            cityEntities.ToList().ForEach(x => results.Add(new CityDetails
            {
                Name = x.Name,
                Description = x.Description,
                Id = x.Id
            }));

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointOfInterests = false)
        {
            var cityToReturn = _cityInfoRepository.GetCity(id, includePointOfInterests);
            if (cityToReturn == null)
            {
                return NotFound();
            }

            var result = new CityDto
            {
                Description = cityToReturn.Description, Name = cityToReturn.Name, Id = cityToReturn.Id,
                PointofInterest = GetMappedPointOfInterest(cityToReturn.PointOfInterest)
            };
            return Ok(result);
        }

        private ICollection<PointOfInterestDto> GetMappedPointOfInterest(IEnumerable<PointOfInterest> pointOfInterest)
        {
            return pointOfInterest.Select(pointOfInterestDto => new PointOfInterestDto {Name = pointOfInterestDto.Name, Description = pointOfInterestDto.Description, Id = pointOfInterestDto.Id}).ToList();
        }
    }
}

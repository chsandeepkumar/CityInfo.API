using CityInfo.API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CityInfo.API.Controller
{
    [Route("api/cities")]
    public class PointOfInterestController : Microsoft.AspNetCore.Mvc.Controller
    {
        [HttpGet("{cityId}/pointofInterest")]
        public IActionResult GetPointOfInterest(int cityId)
        {
            var city = CityDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
            if (city == null) return NotFound();
            return Ok(city.PointofInterest);
        }

        [HttpGet("{cityId}/pointofinterest/{id}"), ActionName("GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null) return NotFound();
            var pointIfInterest = city.PointofInterest.FirstOrDefault(p => p.Id == id);
            if (pointIfInterest == null) return NotFound();
            return Ok(pointIfInterest);
        }
        [HttpPost("{cityId}/pointofinterest")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterest pointOfInterest)
        {
            if (pointOfInterest == null) return BadRequest();
            var city = CityDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
            if (city == null) return NotFound();
            int id = GetCurrentPointOfInterestId();
            var result = new PointOfInterestDto
            {
                Id = ++id,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };
            city.PointofInterest.Add(result);
            return CreatedAtAction("GetPointOfInterest", new { cityId = cityId, id = result.Id}, result);
            }
        

        private static int GetCurrentPointOfInterestId()
        {
            return CityDataStore.Current.Cities.SelectMany(x => x.PointofInterest).Max(x => x.Id);
        }
    }
}

using CityInfo.API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.JsonPatch;

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
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestModel pointOfInterest)
        {
            if (pointOfInterest == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
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
            return CreatedAtAction("GetPointOfInterest", new { cityId = cityId, id = result.Id }, result);
        }

        [HttpPut("{cityId}/pointofInterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id, [FromBody] PointOfInterestModel pointOfInterest)
        {
            if (pointOfInterest == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null) return NotFound();
            var pointIfInterestFromStore = city.PointofInterest.FirstOrDefault(p => p.Id == id);
            if (pointIfInterestFromStore == null) return NotFound();

            pointIfInterestFromStore.Description = pointOfInterest.Description;
            pointIfInterestFromStore.Name = pointOfInterest.Name;

            return NoContent();

        }

        [HttpPatch("{cityId}/pointofInterest/{id}")]
        public IActionResult UpdatePointOfInterestPartially(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestModel> pointofInterestPatchDocument)
        {
            if (pointofInterestPatchDocument == null) return BadRequest();
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null) return NotFound();

            var pointOfInterestFromStore = city.PointofInterest.FirstOrDefault(p => p.Id == id);
            if (pointOfInterestFromStore == null) return NotFound();
            var pointOfInterestToPatch = new PointOfInterestModel
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };

            pointofInterestPatchDocument.ApplyTo(pointOfInterestToPatch, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();


        }

        [HttpDelete("{cityId}/pointofInterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null) return NotFound();

            var pointOfInterestFromStore = city.PointofInterest.FirstOrDefault(p => p.Id == id);
            if (pointOfInterestFromStore == null) return NotFound();

            city.PointofInterest.Remove(pointOfInterestFromStore);
            return NoContent();
        }
        private static int GetCurrentPointOfInterestId()
        {
            return CityDataStore.Current.Cities.SelectMany(x => x.PointofInterest).Max(x => x.Id);
        }
    }
}

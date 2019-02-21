using System.Collections.Generic;
using System.Linq;
using CityInfo.API.Model;
using Microsoft.AspNetCore.Mvc;
namespace CityInfo.API.Controller
{
    [Route("api/cities")]
    public class CitiesController :Microsoft.AspNetCore.Mvc.Controller
    {
        [HttpGet]
        public IActionResult GetCities()
        {
            return new JsonResult(CityDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var cityToReturn = CityDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);
            if (cityToReturn == null)
            {
                return NotFound();
            }

            return Ok(cityToReturn);
        }
    }
}

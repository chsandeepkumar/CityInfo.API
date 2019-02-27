using System.Collections.Generic;
using System.Linq;
using CityInfo.API.DBContext;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _cityInfoContext;

        public CityInfoRepository(CityInfoContext cityInfoContext)
        {
            this._cityInfoContext = cityInfoContext;
        }
        public IEnumerable<City> GetCities()
        {
            return _cityInfoContext.Cities.OrderBy(c => c.Name).ToList();

        }

        public City GetCity(int cityId,bool includePointOfInterest)
        {
            return includePointOfInterest ? _cityInfoContext.Cities.Include(x=>x.PointOfInterest).FirstOrDefault(x => x.Id == cityId) : _cityInfoContext.Cities.FirstOrDefault(x => x.Id == cityId);
        }

        public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId)
        {
          return _cityInfoContext.PointOfInterests.Include(x => x.City).FirstOrDefault(x => x.CityId == cityId && x.Id==pointOfInterestId);
        }

        public IEnumerable<PointOfInterest> GetPointOfInterestsForCity(int cityId)
        {
            return _cityInfoContext.PointOfInterests.Where(p => p.CityId == cityId).ToList();
        }
    }
}

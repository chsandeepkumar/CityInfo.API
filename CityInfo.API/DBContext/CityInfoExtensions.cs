using System.Collections.Generic;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore.Internal;

namespace CityInfo.API.DBContext
{
    public static class CityInfoExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext cityInfoContext)
        {
            if (cityInfoContext.Cities.Any()) return;
            var Cities = new List<City>()
            {
                new City
                {
                    Description = "The one with UCM University",
                    Name = "Missouri",
                    PointOfInterest = new List<PointOfInterest>
                    {
                        new PointOfInterest
                        {
                            Description = "Games",
                            Name = "Sports"
                        }
                    }
                },
                new City
                {
                    Description = "The one with Kansas University",
                    Name = "Kansas",
                    PointOfInterest = new List<PointOfInterest>
                    {
                        new PointOfInterest
                        {
                            Description = "Games",
                            Name = "Sports"
                        },
                        new PointOfInterest
                        {
                            Description = "Fishing",
                            Name = "Hunting"
                        }
                    }
                },
                new City
                {
                    Description = "The one with Technical University",
                    Name = "Ohio",
                    PointOfInterest = new List<PointOfInterest>
                    {
                        new PointOfInterest
                        {
                            Description = "Games",
                            Name = "Sports"
                        },
                        new PointOfInterest
                        {
                            Description = "Games",
                            Name = "Sports"
                        }
                    }
                }
            };
            cityInfoContext.Cities.AddRange(Cities);
            cityInfoContext.SaveChanges();
        }

    }
}

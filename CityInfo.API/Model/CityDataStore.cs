using System.Collections.Generic;

namespace CityInfo.API.Model
{
    public class CityDataStore
    {
        public static CityDataStore Current { get; }=new CityDataStore();
        public List<CityDto> Cities { get; set; }

        public CityDataStore()
        {
            Cities=new List<CityDto>()
            {
                new CityDto
                {
                    Description = "The one with UCM University",
                    Id = 1,
                    Name = "Missouri",
                    PointofInterest = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto
                        {
                            Id = 1,
                            Description = "Games",
                            Name = "Sports"
                        }
                    }
                },
                new CityDto
                {
                    Description = "The one with Kansas University",
                    Id = 2,
                    Name = "Kansas",PointofInterest = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto
                        {
                            Id = 1,
                            Description = "Games",
                            Name = "Sports"
                        },
                        new PointOfInterestDto
                        {
                            Id = 2,
                            Description = "Fishing",
                            Name = "Hunting"
                        }
                    }
                },
                new CityDto
                {
                    Description = "The one with Technical University",
                    Id = 3,
                    Name = "Ohio",
                    PointofInterest = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto
                        {
                            Id = 1,
                            Description = "Games",
                            Name = "Sports"
                        },
                        new PointOfInterestDto
                        {
                            Id = 2,
                            Description = "Games",
                            Name = "Sports"
                        }
                    }
                }

            };
        }
    }
}

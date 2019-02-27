using System;
using System.Collections;
using System.Collections.Generic;

namespace CityInfo.API.Model
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPointInterest => PointofInterest.Count;
        public ICollection<PointOfInterestDto> PointofInterest { get; set; }=new List<PointOfInterestDto>();
    }

    public class CityDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Model
{
    public class PointOfInterestModel
    {
        [Required(ErrorMessage = "Provide a Name filed value")]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
    }
}

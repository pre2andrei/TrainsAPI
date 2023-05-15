using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TrainsAPI.Models
{
    [Index(nameof(Id), IsUnique = true)]
    [Index(nameof(JourneyId), nameof(RoadId), IsUnique = true)]
    public class JourneyRoad

    {
        [Key]
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Display(Name = "Journey")]
        [Required(ErrorMessage = "Journey is required")]
        public int JourneyId { get; set; }
        public Journey? Journey { get; set; }

        [Display(Name = "Road")]
        [Required(ErrorMessage = "Road is required")]
        public int RoadId { get; set; }
        public Road? Road { get; set; }
    }
}

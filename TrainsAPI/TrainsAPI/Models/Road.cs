using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TrainsAPI.Models
{
    [Index(nameof(Id), IsUnique = true)]
    public class Road
    {
        [Key]
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Length is required")]
        public float Length { get; set; }

        [Display(Name = "From")]
        [Required(ErrorMessage = "From is required")]
        public int FromId { get; set; }
        public Station? From { get; set; }


        [Display(Name = "To")]
        [Required(ErrorMessage = "To is required")]
        public int ToId { get; set; }
        public Station? To { get; set; }

        public ICollection<JourneyRoad>? JourneyPaths { get; set; }
    }
}

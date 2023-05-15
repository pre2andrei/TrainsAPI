using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TrainsAPI.Models
{
    [Index(nameof(Id), IsUnique = true)]
    [Index(nameof(Code), IsUnique = true)]
    public class Journey

    {
        [Key]
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is required", AllowEmptyStrings = false)]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Departure is required")]
        public DateTime Departure { get; set; }


        [Display(Name = "Train")]
        [Required(ErrorMessage = "Train is required")]
        public int TrainId { get; set; }
        public Train? Train { get; set; }

        public ICollection<JourneyRoad>? JourneyPaths { get; set; }
    }
}

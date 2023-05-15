using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TrainsAPI.Models
{
    [Index(nameof(Id), IsUnique = true)]
    [Index(nameof(Code), IsUnique = true)]
    public class Train
    {
        [Key]
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is required", AllowEmptyStrings = false)]
        public string Code { get; set; } = string.Empty;

        [Display(Name = "Engine")]
        [Required(ErrorMessage = "Engine is required", AllowEmptyStrings = false)]
        public int EngineId { get; set; }
        public Engine? Engine { get; set; }

        public ICollection<Journey>? Journeys { get; set; }
        public ICollection<Car>? Cars { get; set; }
    }
}

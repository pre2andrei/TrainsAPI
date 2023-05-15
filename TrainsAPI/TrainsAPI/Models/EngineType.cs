using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TrainsAPI.Models
{
    [Index(nameof(Id), IsUnique = true)]
    [Index(nameof(Name), IsUnique = true)]
    public class EngineType
    {
        [Key]
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required", AllowEmptyStrings = false)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Speed is required")]
        public float Speed { get; set; }

        public ICollection<Engine> Engines { get; set;} = new List<Engine>();
    }
}

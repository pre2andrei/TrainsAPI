using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TrainsAPI.Models
{
    [Index(nameof(Id), IsUnique = true)]
    [Index(nameof(Name), IsUnique = true)]
    public class Station
    {
        [Key]
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        public int? X { get; set; }

        public int? Y { get; set; }
    }
}

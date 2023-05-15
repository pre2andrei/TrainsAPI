using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TrainsAPI.Models
{
    [Index(nameof(Id), IsUnique = true)]
    [Index(nameof(Name), IsUnique = true)]
    public class CarType
    {
        [Key]
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required", AllowEmptyStrings = false)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}

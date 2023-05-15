using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TrainsAPI.Models
{
    [Index(nameof(Id), IsUnique = true)]
    [Index(nameof(Code), IsUnique = true)]
    public class Car
    {
        [Key]
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is required", AllowEmptyStrings = false)]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "FirstSeat is required")]
        public int FirstSeat { get; set; }

        [Required(ErrorMessage = "LastSeat is required")]
        public int LastSeat { get; set; }


        [Display(Name = "Car Type")]
        [Required(ErrorMessage = "Car Type is required")]
        public int CarTypeId { get; set; }
        public CarType? CarType { get; set; }


        [Display(Name = "Train")]
        public int? TrainId { get; set; }
        public Train? Train { get; set; }
    }
}

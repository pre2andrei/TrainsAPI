using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TrainsAPI.Models
{
    [Index(nameof(Id), IsUnique = true)]
    [Index(nameof(Code), IsUnique = true)]
    public class Engine
    {
        [Key]
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is required", AllowEmptyStrings = false)]
        public string Code { get; set; } = string.Empty;


        [Display(Name = "Engine Type")]
        [Required(ErrorMessage = "Engine Type is required")]
        public int EngineTypeId { get; set; }
        public EngineType? EngineType { get; set; }

        public Train? Train { get; set; }
    }
}



using System.ComponentModel.DataAnnotations;

namespace TrainsAPI.Models.Authentication.SignUp
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string? Password { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "User Name is required")]
        public string? Email { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace TrainsAPI.Models.Authentication.Login
{
    public class LoginUser
    {
        [Required(ErrorMessage = "User Name required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password required")]
        public string? Password { get; set; }
    }
}

using FirstAPI.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace FirstAPI.Dao
{
    public class UserDAO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; } = " ";

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = " ";

        public string? ProfilePicture { get; set; }

        public string? Bio { get; set; }

    }
}

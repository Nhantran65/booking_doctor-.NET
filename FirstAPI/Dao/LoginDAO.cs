using FirstAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace FirstAPI.Dao
{
    public class LoginDAO
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = " ";

        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; } = " ";


    }
}

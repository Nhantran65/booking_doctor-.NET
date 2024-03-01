using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FirstAPI.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FirstAPI.Models;
[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "FirstName is required")]
    [MaxLength(255)]
    [Column("first_name")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "LastName is required")]
    [MaxLength(255)]
    [Column("last_name")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [MaxLength(255)]
    [EmailAddress(ErrorMessage ="Invalid Email")]
    [Column("email")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [Column("password_hash")]
    public string? PasswordHash { get; set; }

    [Column("role")]
    public UserRole Role { get; set; }


    [Column("profile_picture")]
    public string? ProfilePicture { get; set; }

    [Column("bio")]
    public string? Bio { get; set; }
}



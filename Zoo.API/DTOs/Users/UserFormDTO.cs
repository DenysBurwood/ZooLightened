using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zoo.API.DTOs.Users
{
    public class UserFormDTO
    {
        [Required]
        [MaxLength(60,ErrorMessage = "Maximum 60 characters")]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(60,ErrorMessage = "Maximum 60 characters")]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "A strong password is required")]
        [PasswordPropertyText]
        public string Password { get; set; } = null!;
    }
}

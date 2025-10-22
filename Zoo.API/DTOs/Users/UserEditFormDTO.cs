using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zoo.API.DTOs.Users
{
    public class UserEditFormDTO
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "A strong password is required")]
        [PasswordPropertyText]
        public string Password { get; set; } = null!;
    }
}

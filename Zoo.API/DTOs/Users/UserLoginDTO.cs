using System.ComponentModel.DataAnnotations;

namespace Zoo.API.DTOs.Users
{
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "An email address is required")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "A password is required")]
        public string Password { get; set; } = null!;
    }
}

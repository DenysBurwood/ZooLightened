using System.ComponentModel.DataAnnotations;

namespace Zoo.API.DTOs.Users
{
    public class EmailEditFormDTO
    {
        [Required]
        [MinLength(1), MaxLength(60)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(1), MaxLength(60)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string OldEmail { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string NewEmail { get; set; } = null!;

    }
}

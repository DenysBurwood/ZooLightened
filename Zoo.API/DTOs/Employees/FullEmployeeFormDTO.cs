using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Zoo.DL.Enum;

namespace Zoo.API.DTOs.Employees
{
    public class FullEmployeeFormDTO
    {

        //  EmployeeFormDTO
        [Required]
        public EmployeeType EmployeeType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }


        //  UserFormDTO
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "A strong password is required")]
        [PasswordPropertyText]
        public string Password { get; set; } = null!;


        //  AddressFormDTO
        [Required]
        [Length(3, 100, ErrorMessage = "Required field containing between 3 and 100 characters.")]
        public string Street { get; set; } = null!;

        //  What about postal boxes
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A positive integer")]
        public int Number { get; set; }

        [Required]
        [Length(3,100,ErrorMessage = "Required field containing between 3 and 100 characters.")]
        public string City { get; set; } = null!;

        //  What about letters in postalcodes of other countries
        [Required]
        [Range(1, 999999, ErrorMessage = "A positive integer")]
        public int PostalCode { get; set; }

        [Required]
        [Length(3,100,ErrorMessage = "Required field containing between 3 and 100 characters.")]
        public string Country { get; set; } = null!;
    }
}

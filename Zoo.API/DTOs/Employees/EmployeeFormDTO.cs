using System.ComponentModel.DataAnnotations;
using Zoo.DL.Entities;
using Zoo.DL.Entities.Humans;
using Zoo.DL.Enum;

namespace Zoo.API.DTOs.Employees
{
    public class EmployeeFormDTO
    {
        [Required(ErrorMessage = "This field is required")]
        public int AddressId { get; set; }

        [Required]
        public EmployeeType EmployeeType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}

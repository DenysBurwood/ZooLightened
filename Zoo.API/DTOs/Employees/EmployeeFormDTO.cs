using System.ComponentModel.DataAnnotations;
using Zoo.DL.Enum;

namespace Zoo.API.DTOs.Employees
{
    public class EmployeeFormDTO
    {
        [Required]
        [Range(1,int.MaxValue,ErrorMessage = "A positive owner Id")]
        public int AddressId { get; set; }

        [Required]
        public EmployeeType EmployeeType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; } = null!;
    }
}

using Zoo.DL.Entities;
using Zoo.DL.Entities.Humans;

namespace Zoo.API.DTOs.Employees
{
    public class EmployeeAccountDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Address Address { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string EmployeeType { get; set; } = null!;
        public User User { get; set; } = null!;

    }
}

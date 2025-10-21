using Zoo.DL.Entities;
using Zoo.DL.Entities.Humans;
using Zoo.DL.Enum;

namespace Zoo.API.DTOs
{
    public class EmployeeAccountDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Address Address { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public User User { get; set; } = null!;

    }
}

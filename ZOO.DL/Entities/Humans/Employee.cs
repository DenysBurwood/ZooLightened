using Zoo.DL.Enum;

namespace Zoo.DL.Entities.Humans
{
    public class Employee : BaseEntity
    {
        public Address Address { get; set; } = null!;

        public int AddressId { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public User User { get; set; } = null!;
        public int UserId { get; set; }
    }
}

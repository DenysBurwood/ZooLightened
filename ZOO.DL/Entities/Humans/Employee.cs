using Zoo.DL.Enum;

namespace Zoo.DL.Entities.Humans
{
    public class Employee:User
    {
        public Address Address { get; set; } = null!;
        public EmployeeType EmployeeType { get; set; }
    }
}

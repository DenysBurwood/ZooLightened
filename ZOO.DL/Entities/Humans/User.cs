
namespace Zoo.DL.Entities.Humans
{
    public class User//:BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public bool IsSubscribed { get; set; }

    }
}

namespace Zoo.API.DTOs
{
    public class UserAccountDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int? EmployeeId { get; set; }
        public bool IsSubscribed { get; set; }
    }
}

using Zoo.DL.Entities.Humans;

namespace Zoo.DL.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; } = null!;
        public int Number { get; set; }
        public string City { get; set; } = null!;
        public int PostalCode { get; set; }
        public string Country { get; set; } = null!;
        public List<Employee> Employees { get; set; } = [];
    }
}


namespace Zoo.API.DTOs.Animals
{
    public class AnimalDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Sex { get; set; } = null!;

        public int SexId { get; set; }

        public string SpeciesName { get; set; } = null!;
        public string? OwnerName { get; set; } = null!;

        public int? OwnerId { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? RIPDate { get; set; }
    }
}

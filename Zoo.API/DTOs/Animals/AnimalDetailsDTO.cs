using Zoo.DL.Entities;
using Zoo.DL.Enum;

namespace Zoo.API.DTOs.Animals
{
    public class AnimalDetailsDTO
    {
        public string Name { get; set; } = null!;
        public string Sex { get; set; } = null!;
        public string SpeciesName { get; set; } = null!;
        public string? OwnerName { get; set; } = null!;
        public bool IsAvailable { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? RIPDate { get; set; }
    }
}

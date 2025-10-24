using Zoo.DL.Enum;

namespace Zoo.API.DTOs.Animals
{
    public class AnimalIndexDTO
    {
        public string Name { get; set; } = null!;
        public string SpeciesName { get; set; } = null!;
        public string Sex { get; set; }
    }
}

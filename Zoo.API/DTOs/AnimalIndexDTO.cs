using Zoo.DL.Enum;

namespace Zoo.API.DTOs
{
    public class AnimalIndexDTO
    {
        public string Name { get; set; } = null!;
        public string SpeciesName { get; set; } = null!;
        public Sex Sex { get; set; }
    }
}

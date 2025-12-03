
namespace Zoo.API.DTOs.Animals
{
    public class AnimalIndexDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string SpeciesName { get; set; } = null!;
        public string Sex { get; set; }

        public int SexId { get; set; }
    }
}

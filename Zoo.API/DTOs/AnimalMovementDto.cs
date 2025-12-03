namespace Zoo.API.DTOs
{
    public class AnimalMovementDto
    {
        public int Id { get; set; }

        public string Type { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int AnimalId { get; set; }

        public string AnimalName { get; set; } = null!;

        public string SpeciesName { get; set; } = null!;

        public string OwnerName { get; set; } = null!;

        public string CounterPartName { get; set; } = null!;
    }
}

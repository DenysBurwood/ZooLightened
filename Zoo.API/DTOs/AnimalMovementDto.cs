namespace Zoo.API.DTOs
{
    public class AnimalMovementDto
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int AnimalId { get; set; }

        public string AnimalName { get; set; }

        public string SpeciesName { get; set; }

        public string OwnerName { get; set; }
    }
}

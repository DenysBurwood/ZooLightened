using Zoo.DL.Enum;

namespace Zoo.DL.Entities
{
    public class Animal : BaseEntity
    {
        public string Name { get; set; } = null!;
        public Sex Sex { get; set; }
        public int SpeciesId { get; set; }

        public AnimalSpecies Species { get; set; } = null!;

        public int OwnerId { get; set; }

        public Owner? Owner { get; set; } = null!;

        public bool IsAvailable { get; set; }

        public DateTime BirthDate {  get; set; }

        public DateTime? RIPDate {  get; set; }

        public List<AnimalMovement>? AnimalMovements { get; set; }
    }
}

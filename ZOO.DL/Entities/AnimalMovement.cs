using Zoo.DL.Enum;

namespace Zoo.DL.Entities
{
    public class AnimalMovement : BaseEntity
    {

        public Animal Animal { get; set; } = null!;

        public int AnimalId { get; set; }

        public Direction Direction { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public AnimalMovementType Type { get; set; }

        public Owner CounterPart { get; set; }

        public int CounterPartId { get; set; }

    }
}

using Zoo.DL.Enum;

namespace Zoo.API.DTOs
{
    public class AnimalFormDTO
    {
        public string Name { get; set; } = null!;
        public Sex Sex { get; set; }
        public int SpeciesId { get; set; }
        //  toys and other tables after
        //  public Toys Toy {get set}
        //  public string Photos
        //  public int age
    }
}

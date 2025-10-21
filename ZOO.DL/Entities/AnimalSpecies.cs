
namespace Zoo.DL.Entities
{
    public class AnimalSpecies : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;


        public List<Animal>? Animals { get; set; }
    }
}

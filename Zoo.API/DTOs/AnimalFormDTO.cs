using System.ComponentModel.DataAnnotations;
using Zoo.DL.Enum;

namespace Zoo.API.DTOs
{
    public class AnimalFormDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Minimum size of 3 characters")]
        public string Name { get; set; } = null!;
        public Sex Sex { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ("A positive species Id"))]
        public int SpeciesId { get; set; }
        //  toys and other tables after
        //  public Toys Toy {get set}
        //  public string Photos
        //  public int age
    }
}

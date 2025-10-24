using System.ComponentModel.DataAnnotations;
using Zoo.DL.Enum;

namespace Zoo.API.DTOs.Animals
{
    public class AnimalFormDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Minimum size of 3 characters")]
        public string Name { get; set; } = null!;
        public Sex Sex { get; set; }

        [Required]
        public string SpeciesName { get; set; } = null!;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A positive owner Id")]
        public int OwnerId { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
        public DateTime? RIPDate { get; set; }
    }
}

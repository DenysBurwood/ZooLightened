using System.ComponentModel.DataAnnotations;
using Zoo.DL.Enum;

namespace Zoo.API.DTOs.Animals
{
    public class AnimalRentFormDto
    {
        [Required]
        [Range(1,int.MaxValue,ErrorMessage = "A positive owner Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(60, ErrorMessage = "Maximum 60 characters")]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }
}

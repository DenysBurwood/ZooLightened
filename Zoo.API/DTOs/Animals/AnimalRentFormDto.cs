using System.ComponentModel.DataAnnotations;
using Zoo.DL.Enum;

namespace Zoo.API.DTOs.Animals
{
    public class AnimalRentFormDto
    {
        [Required]

        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }
}

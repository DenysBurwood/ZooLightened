using System.ComponentModel.DataAnnotations;

namespace Zoo.API.DTOs.Animals
{
    public class AnimalHireFormDto
    {

        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }
}

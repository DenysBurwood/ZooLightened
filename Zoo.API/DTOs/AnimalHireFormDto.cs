using System.ComponentModel.DataAnnotations;

namespace Zoo.API.DTOs
{
    public class AnimalHireFormDto
    {
        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }
}

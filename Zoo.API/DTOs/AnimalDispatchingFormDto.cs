using System.ComponentModel.DataAnnotations;

namespace Zoo.API.DTOs
{
    public class AnimalDispatchingFormDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A positive integer")]
        public int AnimalId { get; set; }

        [Required]
        public DateTime DispatchingDate { get; set; } = DateTime.Now;
    }
}

using System.ComponentModel.DataAnnotations;

namespace Zoo.API.DTOs
{
    public class AnimalDispatchingFormDto
    {
        [Required]
        public int AnimalId { get; set; }

        [Required]
        public DateTime DispatchingDate { get; set; } = DateTime.Now;
    }
}

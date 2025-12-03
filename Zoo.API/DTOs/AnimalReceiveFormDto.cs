using System.ComponentModel.DataAnnotations;

namespace Zoo.API.DTOs
{
    public class AnimalReceptionFormDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A positive integer")]
        public int AnimalId { get; set; }

        [Required]
        public DateTime ReceptionDate { get; set; } = DateTime.Now;
    }
}

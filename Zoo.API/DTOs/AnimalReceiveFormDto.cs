using System.ComponentModel.DataAnnotations;

namespace Zoo.API.DTOs
{
    public class AnimalReceptionFormDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime ReceptionDate { get; set; } = DateTime.Now;
    }
}

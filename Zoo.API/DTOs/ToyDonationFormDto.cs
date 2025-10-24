using System.ComponentModel.DataAnnotations;

namespace Zoo.API.DTOs
{
    public class ToyDonationFormDto
    {

        [Required]
        public int ToyId { get; set; }

        [Required]
        public int UserId { get; set; }

        public DateTime DonationDate { get; set; } = DateTime.Now;

        [Required]
        public int Amount { get; set; }
    }
}
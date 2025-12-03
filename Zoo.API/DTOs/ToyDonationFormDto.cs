using System.ComponentModel.DataAnnotations;

namespace Zoo.API.DTOs
{
    public class ToyDonationFormDto
    {

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A positive integer")]
        public int ToyId { get; set; }

        [Required]
        [Range(1,int.MaxValue,ErrorMessage = "A positive integer")]
        public int UserId { get; set; }

        public DateTime DonationDate { get; set; } = DateTime.Now;

        [Required]
        [Range(1,int.MaxValue,ErrorMessage = "A positive integer")]
        public int Amount { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Zoo.API.DTOs.Animals
{
    public class AnimalHireFormDto
    {

        [Required]
        [Range(1,int.MaxValue,ErrorMessage = "A positive owner Id")]
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }
}

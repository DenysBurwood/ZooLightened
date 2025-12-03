using Zoo.DL.Enum;

namespace Zoo.API.DTOs
{
    public class ToyIndexDto
    {
        public int Id { get; set; }

        public string SpeciesName { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? ImagePath { get; set; } = null!;

        public int MinimumAmountperDonation { get; set; }

        public int WishedTotalAmount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ToyStatus Status { get; set; }

        public decimal TotalAmountSoFar { get; set; }
    }
}
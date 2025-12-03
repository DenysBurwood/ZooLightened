using Zoo.DL.Entities.Humans;

namespace Zoo.DL.Entities
{
    public class ToyDonation :BaseEntity
    {

        public int ToyId { get; set; }

        public Toy Toy { get; set; } = null!;

        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public int Amount { get; set; }

        public DateTime DonationDate { get; set; }
    }
}

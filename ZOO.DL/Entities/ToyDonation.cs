using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.DL.Entities.Humans;

namespace Zoo.DL.Entities
{
    public class ToyDonation :BaseEntity
    {

        public int ToyId { get; set; }

        public Toy Toy { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int Amount { get; set; }

        public DateTime DonationDate { get; set; }
    }
}

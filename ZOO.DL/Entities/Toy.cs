using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.DL.Enum;

namespace Zoo.DL.Entities
{
    public class Toy :BaseEntity
    {
        public int SpeciesId { get; set; }

        public AnimalSpecies Species { get; set; } = null!;

        public List<ToyDonation>? Donations { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? ImagePath { get; set; } = null!;

        public int MinimumAmountperDonation { get; set; }

        public int WishedTotalAmount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ToyStatus Status { get; set; }

    }
}

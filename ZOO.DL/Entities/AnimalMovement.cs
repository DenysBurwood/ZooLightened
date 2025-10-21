using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.DL.Enum;

namespace Zoo.DL.Entities
{
    public class AnimalMovement
    {
        public int Id { get; set; }

        public Animal Animal { get; set; } = null!;

        public int AnimalId { get; set; }

        public Direction Direction { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }
}

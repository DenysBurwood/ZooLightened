using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.DL.Enum;

namespace Zoo.DL.Entities
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Sex Sex { get; set; }
        public int SpeciesId { get; set; }
        //  toys and other tables after
        //  public Toys Toy {get set}
        //  public string Photos
        //  public int age
        //  ...

        public AnimalSpecies Species { get; set; } = null!;
    }
}

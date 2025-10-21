using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.DL.Entities
{
    public class Owner : BaseEntity
    {

        public string Name { get; set; } = null!;

        public Address Address { get; set; } = null!;

        public string ContactName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        public List<Animal>? Animals { get; set; }
    }
}

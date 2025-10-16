using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.BLL.Exceptions
{
    public class AnimalNotFoundException:NotFoundException
    {
        public AnimalNotFoundException(string message) : base(message)
        {
        }
        public AnimalNotFoundException() : base("No Animal found") 
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.BLL.Exceptions
{
    public class ToyNotFoundException : NotFoundException
    {
        public ToyNotFoundException(string message) : base(message)
        {
        }
        public ToyNotFoundException() : base("No Toy found")
        {
        }
    }
}

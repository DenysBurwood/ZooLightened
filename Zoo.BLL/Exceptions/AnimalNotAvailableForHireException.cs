using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.BLL.Exceptions
{
    public class AnimalNotAvailableForHireException :AnimalNotAvailableException
    {
        public AnimalNotAvailableForHireException(string message) : base(message)
        {
        }
        public AnimalNotAvailableForHireException() : base("This animal is not available for hire.")
        {
        }
    }
}

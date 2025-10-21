using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.BLL.Exceptions
{
    public class AnimalNotAvailableForRentException:AnimalNotAvailableException
    {
        public AnimalNotAvailableForRentException(string message) : base(message)
        {
        }
        public AnimalNotAvailableForRentException() : base("This animal is not available for rent.")
        {
        }
    }
}

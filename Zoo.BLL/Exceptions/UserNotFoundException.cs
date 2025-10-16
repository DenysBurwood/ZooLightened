using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.BLL.Exceptions
{
    public class UserNotFoundException:NotFoundException
    {
        public UserNotFoundException(string message) : base(message)
        {
        }
        public UserNotFoundException() : base("User not found") 
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.BLL.Exceptions
{
    public class RegisterException:ZooException
    {
        public RegisterException(string message) : base(401,message)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.BLL.Exceptions
{
    public class LoginException:ZooException
    {
        public LoginException() : base (418,"Login or password wrong")
        {
        
        }
        public LoginException(string message) : base(418,message)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.BLL.Exceptions
{
    public class NotAllowedException:ZooException
    {
        public NotAllowedException(string message) : base(402,message)
        {
        }
    }
}

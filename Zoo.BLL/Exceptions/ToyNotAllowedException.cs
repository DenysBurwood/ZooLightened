using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.BLL.Exceptions
{
    public class ToyNotAllowedException : NotAllowedException
    {
        public ToyNotAllowedException(string message) : base(message)
        {
        }
    }
}

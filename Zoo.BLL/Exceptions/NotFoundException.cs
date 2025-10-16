using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.BLL.Exceptions
{
    public class NotFoundException:ZooException
    {
        public NotFoundException(object content) : base(403,content)
        {

        }
    }
}

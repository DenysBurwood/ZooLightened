using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.BLL.Exceptions
{
    public abstract class ZooException:Exception
    {
        public int StatusCode { get; set; }
        public Object Content { get; set; } = null!;

        public ZooException(int statusCode, Object content) 
        {
            StatusCode = statusCode;
            Content= content;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Zoo.DL.Enum;

namespace Zoo.DL.Entities.Humans
{
    public class Employee:User
    {
        public Address Address { get; set; } = null!;
        public EmployeeType EmployeeType { get; set; }
    }
}

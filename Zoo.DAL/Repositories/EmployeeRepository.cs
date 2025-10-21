using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.DAL.Contexts;
using Zoo.DL.Entities.Humans;

namespace Zoo.DAL.Repositories
{
    public class EmployeeRepository
    {
        private ZooContext _context;
        private DbSet<Employee> _employees;
        public EmployeeRepository(ZooContext context) 
        {
            _context=context;
            _employees=context.Employees;
        }

        public Employee? GetEmployeeByEmployeeId(int employeeId) 
        {
            return _employees.FirstOrDefault(y => y.Id==employeeId);
        }
    }
}

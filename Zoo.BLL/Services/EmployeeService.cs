using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.DAL.Repositories;
using Zoo.DL.Entities.Humans;

namespace Zoo.BLL.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;
        public EmployeeService(EmployeeRepository employeeRepository) 
        {
            _employeeRepository = employeeRepository;
        }

        public Employee? GetEmployeeByEmployeeId(int EmployeeId) 
        {
            return _employeeRepository.GetEmployeeByEmployeeId(EmployeeId);
        }
    }
}

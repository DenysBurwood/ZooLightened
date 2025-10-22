using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.BLL.Exceptions;
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

        public void CreateEmployeeSheet(Employee employee) 
        {
            if(employee is null) 
            {
                throw new EmployeeNotFound("No employee was found");
            }
            if(employee.User is null) 
            {
                throw new NotFoundException("Employee.User is null");
            }
            _employeeRepository.Add(employee);
        }

        public void CreateNewEmployee(Employee employee) 
        {
            _employeeRepository.Add(employee);
        }

    }
}

using Zoo.BLL.Exceptions;
using Zoo.DAL.Repositories;
using Zoo.DL.Entities.Humans;

namespace Zoo.BLL.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly UserRepository _userRepository;
        public EmployeeService(EmployeeRepository employeeRepository, UserRepository userRepository) 
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
        }

        public Employee? GetEmployeeByEmployeeId(int EmployeeId) 
        {
            return _employeeRepository.GetEmployeeByEmployeeId(EmployeeId);
        }

        public Employee CreateEmployeeSheet(Employee employee) 
        {
            if(employee is null) 
            {
                throw new EmployeeNotFound("No employee was found");
            }
            Employee newEmployee = _employeeRepository.AddEmployeeSheet(employee.AddressId, employee.EmployeeType, employee.StartDate, employee.EndDate, employee.UserId);
            return newEmployee;
        }

        public void CreateNewEmployee(Employee employee) 
        {
            _employeeRepository.Add(employee);
        }

        public void FireEmployee(Employee employee, int employeeId) 
        {
            _employeeRepository.FireEmployee(employee);
        }
    }
}

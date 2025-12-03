using Microsoft.EntityFrameworkCore;
using Zoo.DAL.Contexts;
using Zoo.DL.Entities.Humans;
using Zoo.DL.Enum;

namespace Zoo.DAL.Repositories
{
    public class EmployeeRepository:BaseRepository<Employee>
    {
        private ZooContext _context;
        private DbSet<Employee> _employees;
        public EmployeeRepository(ZooContext context):base(context)
        {
            _context=context;
            _employees=context.Employees;
        }

        public Employee AddEmployeeSheet(int addressId, EmployeeType employeeType, DateTime startDate, DateTime? endDate, int userId) 
        {
            Employee employee = new Employee() 
            {
                AddressId=addressId,
                EmployeeType=employeeType,
                StartDate=startDate,
                EndDate=endDate,
                UserId=userId,
            };
            _employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public void FireEmployee(Employee employee) 
        {
            employee.EndDate=DateTime.Now;
            _employees.Update(employee);
            _context.SaveChanges();
        }

        public Employee? GetEmployeeByEmployeeId(int employeeId) 
        {
            return _employees.FirstOrDefault(y => y.Id==employeeId);
        }
        public Employee? GetEmployeeByUserId(int userId)
        {
            return _employees.FirstOrDefault(y => y.UserId==userId);
        }

    }
}

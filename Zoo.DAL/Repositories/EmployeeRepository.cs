using Microsoft.EntityFrameworkCore;
using Zoo.DAL.Contexts;
using Zoo.DL.Entities.Humans;

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

        public Employee? GetEmployeeByEmployeeId(int employeeId) 
        {
            return _employees.FirstOrDefault(y => y.Id==employeeId);
        }
        public Employee? GetEmployeeByUserId(int userId)
        {
            return _employees.FirstOrDefault(y => y.UserId==userId);
        }

        //public override void Add(Employee entity)
        //{
        //    _employees.Add(entity);
        //    _context.SaveChanges();
        //}
    }
}

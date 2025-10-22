
namespace Zoo.BLL.Exceptions
{
    public class EmployeeNotFound:NotFoundException
    {
        public EmployeeNotFound(string content) : base(content)
        {
        }
        public EmployeeNotFound() : base("Employee not found.")
        {
        
        }
    }
}

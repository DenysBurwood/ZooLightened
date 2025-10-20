
namespace Zoo.BLL.Exceptions
{
    public class UserNotFoundException:NotFoundException
    {
        public UserNotFoundException(string message) : base(message)
        {
        }
        public UserNotFoundException() : base("User not found") 
        {
        }
    }
}

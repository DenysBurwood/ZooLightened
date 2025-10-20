
namespace Zoo.BLL.Exceptions
{
    public class RegisterException:ZooException
    {
        public RegisterException(string message) : base(401,message)
        {
        }
    }
}

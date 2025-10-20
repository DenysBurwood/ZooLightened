
namespace Zoo.BLL.Exceptions
{
    public class LoginException:ZooException
    {
        public LoginException() : base (418,"Login or password wrong")
        {
        
        }
        public LoginException(string message) : base(418,message)
        {
        }
    }
}

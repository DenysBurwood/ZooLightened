
namespace Zoo.BLL.Exceptions
{
    public class NotAllowedException:ZooException
    {
        public NotAllowedException(string message) : base(402,message)
        {
        }
    }
}

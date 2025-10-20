
namespace Zoo.BLL.Exceptions
{
    public class NotFoundException:ZooException
    {
        public NotFoundException(object content) : base(403,content)
        {

        }
    }
}


namespace Zoo.BLL.Exceptions
{
    public class AnimalNotAvailableException :ZooException
    {
        public AnimalNotAvailableException(object content) : base(410, content)
        {

        }
    }
}

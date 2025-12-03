
namespace Zoo.BLL.Exceptions
{
    public class ToyNotFoundException : NotFoundException
    {
        public ToyNotFoundException(string message) : base(message)
        {
        }
        public ToyNotFoundException() : base("No Toy found")
        {
        }
    }
}

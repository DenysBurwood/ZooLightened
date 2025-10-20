
namespace Zoo.BLL.Exceptions
{
    public class AnimalNotFoundException:NotFoundException
    {
        public AnimalNotFoundException(string message) : base(message)
        {
        }
        public AnimalNotFoundException() : base("No Animal found") 
        {
        }
    }
}

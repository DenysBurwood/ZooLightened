using Microsoft.EntityFrameworkCore;
using Zoo.DAL.Contexts;
using Zoo.DL.Entities;

namespace Zoo.DAL.Repositories
{
    public class AddressRepository:BaseRepository<Address>
    {
        private readonly ZooContext _context;
        private readonly DbSet<Address> _addresses;

        public AddressRepository (ZooContext context):base (context)
        {
            _context=context;
            _addresses=context.Addresses;
        }
    }
}

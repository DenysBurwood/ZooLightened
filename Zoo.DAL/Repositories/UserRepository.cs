using Microsoft.EntityFrameworkCore;
using Zoo.DAL.Contexts;
using Zoo.DL.Entities.Humans;

namespace Zoo.DAL.Repositories
{
    public class UserRepository
    {
        private readonly ZooContext _context;
        private readonly DbSet<User> _users;
        public UserRepository(ZooContext zooContext)
        {
            _context=zooContext;
            _users=zooContext.Users;
        }

        public void Add(User entity)
        {
            //if (entity.)
            _users.Add(entity);
            _context.SaveChanges();
        }

        public void Update(int id,User entity)
        {
            User? user=_users.FirstOrDefault(x => x.Id==id);
            if(user!=null) 
            {
                _users.Update(entity);
                _context.SaveChanges();
            }
        }
        public User? GetByEmail(string email) 
        {
            return _users.FirstOrDefault(y => y.Email==email);
        }
        public User? GetById(int id) 
        {
            return _users.FirstOrDefault(y => y.Id==id);
        }
        public void Subscribe(User user) 
        {
            user.IsSubscribed= true;
            _users.Update(user);
            _context.SaveChanges();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Zoo.DAL.Contexts;
using Zoo.DL.Entities.Humans;

namespace Zoo.DAL.Repositories
{
    public class UserRepository:BaseRepository<User>
    {
        private readonly ZooContext _context;
        private readonly DbSet<User> _users;
        public UserRepository(ZooContext zooContext):base(zooContext)
        {
            _context=zooContext;
            _users=zooContext.Users;
        }

        public User? GetByEmail(string email) 
        {
            return _users.FirstOrDefault(y => y.Email==email);
        }

        public void Subscribe(User user) 
        {
            user.IsSubscribed= true;
            _users.Update(user);
            _context.SaveChanges();
        }

        public override void Update(User user) 
        {
            User CurrentUser = _users.FirstOrDefault(u => u.Email==user.Email)!;
            CurrentUser.FirstName=user.FirstName;
            CurrentUser.LastName=user.LastName;
            CurrentUser.Email=user.Email;
            CurrentUser.Password=user.Password;
            _context.SaveChanges();
        }

    }
}

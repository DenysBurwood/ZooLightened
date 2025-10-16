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

        //public override User MapEntity(SqlDataReader reader)
        //{
        //    User user = new User()
        //    {
        //        Id=(int)reader["id"],
        //        FirstName=(string)reader["firstname"],
        //        LastName=(string)reader["lastname"],
        //        Email=(string)reader["email"],
        //        Password=(string)reader["password"],
        //        IsEmployee=(bool)reader["isEmployee"],
        //    };
        //    return user;
        //}

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
    }
}

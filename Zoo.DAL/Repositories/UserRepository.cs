using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.DAL.Contexts;
using Zoo.DL.Entities.Humans;

namespace Zoo.DAL.Repositories
{
    public class UserRepository:BaseRepository<User,int>
    {
        private readonly ZooContext _context;
        private readonly DbSet<User> _users;
        public UserRepository(ZooContext zooContext)
        {
            _context=zooContext;
            _users=zooContext.Users;
        }
        protected override string TableName => "User";

        protected override string ColumnIdName => "id.";

        public override void Add(User entity)
        {
            _users.Add(entity);
            _context.SaveChanges();
        }

        public override User MapEntity(SqlDataReader reader)
        {
            User user = new User()
            {
                Id=(int)reader["id"],
                FirstName=(string)reader["firstname"],
                LastName=(string)reader["lastname"],
                Email=(string)reader["email"],
                Password=(string)reader["password"],
                IsEmployee=(bool)reader["isEmployee"],
            };
            return user;
        }

        public override void Update(int id,User entity)
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

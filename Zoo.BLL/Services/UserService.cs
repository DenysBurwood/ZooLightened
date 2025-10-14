using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.DAL.Repositories;
using Zoo.DL.Entities.Humans;

namespace Zoo.BLL.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public void Register(User user) 
        {
            _userRepository.Add(user);
        }
        public User? Login(string email) 
        {
            User? user = _userRepository.GetByEmail(email);
            return user;
        }
    }
}

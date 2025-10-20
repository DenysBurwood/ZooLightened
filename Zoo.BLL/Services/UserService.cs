using Isopoh.Cryptography.Argon2;
using Zoo.BLL.Exceptions;
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
            if(_userRepository.GetByEmail(user.Email) is not null)
            {
                    throw new RegisterException("Email already registered");
            }
            //  Hash of password
            user!.Password=Argon2.Hash(user.Password);
            _userRepository.Add(user);
        }
        public User Login(string email, string password) 
        {
            User? user = _userRepository.GetByEmail(email);
            if(user is null)
            {
                throw new LoginException("Bad login or password.");
            }
            if(!Argon2.Verify(user.Password,password))
            {
                throw new LoginException("Bad login or password.");
            }
            return user;
        }
        public void Subscribe(int id) 
        {
            User? user = _userRepository.GetById(id);
            if(user is null) 
            {
                throw new UserNotFoundException($"User with id: {id} not found.");
            }
            if(user.IsSubscribed) 
            {
                throw new NotAllowedException("Already subscribed.");
            }
            _userRepository.Subscribe(user);
        }
    }
}

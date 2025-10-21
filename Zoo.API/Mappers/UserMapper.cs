using Zoo.API.DTOs;
using Zoo.DL.Entities.Humans;

namespace Zoo.API.Mappers
{
    public static class UserMapper
    {
        public static UserFormDTO ToUserForm(this User user) 
        {
            return new UserFormDTO() 
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
            };
        }
        public static User FromUserForm(this UserFormDTO user) 
        {
            return new User() 
            {
                FirstName= user.FirstName,
                LastName= user.LastName,
                Email = user.Email,
                Password = user.Password,
            };
        }

        public static UserAccountDTO ToUserAccountDTO(this User user) 
        {
            return new UserAccountDTO()
            {
                FirstName=user.FirstName,
                LastName=user.LastName,
                Email = user.Email,
                EmployeeId = user.EmployeeId,
                IsSubscribed = user.IsSubscribed,
            };
        }
    }
}

using Zoo.API.DTOs.Employees;
using Zoo.DL.Entities;
using Zoo.DL.Entities.Humans;

namespace Zoo.API.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeAccountDTO ToEmployeeAccountDTO(this Employee employee, User user, Address address) 
        {
            return new EmployeeAccountDTO()
            {
                FirstName=employee.User.FirstName,
                LastName=employee.User.LastName,
                Email=employee.User.Email,
                Address=new Address() 
                {
                    Street=address.Street,
                    City=address.City,
                    PostalCode=address.PostalCode,
                    Country=address.Country,
                    Number=address.Number,
                },
                StartDate=employee.StartDate,
                EndDate=employee.EndDate,
                EmployeeType=employee.EmployeeType.ToString(),
                User=new User() 
                {
                    FirstName=user.FirstName,
                    LastName =user.LastName,
                    Email=user.Email,
                    Password="Password",
                    IsSubscribed=user.IsSubscribed,
                }
            };
        }

        public static EmployeeFormDTO ToEmployeeFormDto(this Employee employee) 
        {
            return new EmployeeFormDTO()
            {
                AddressId=employee.AddressId,
                EmployeeType = employee.EmployeeType,
                StartDate = employee.StartDate,
                EndDate = employee.EndDate,
                UserEmail=employee.User.Email,
            };
        }
        public static Employee FromEmployeeFormDTO(this EmployeeFormDTO employee) 
        {
            return new Employee()
            {
                AddressId=employee.AddressId,
                EmployeeType=employee.EmployeeType,
                StartDate=employee.StartDate,
                EndDate=employee.EndDate,
            };
        }

        public static Employee FromFullEmployeeFormDTO(this FullEmployeeFormDTO employee) 
        {
            return new Employee()
            {
                EmployeeType=employee.EmployeeType,
                StartDate=employee.StartDate,
                EndDate=employee.EndDate,
                //UserId=employee
                User=employee.UserFromFullEmployeeFormDTO(),
                Address=employee.AddressFromFullEmployeeFormDTO(),
            };
        }

        public static Address AddressFromFullEmployeeFormDTO(this FullEmployeeFormDTO employee) 
        {
            return new Address()
            {
                Street=employee.Street,
                City=employee.City,
                Number=employee.Number,
                PostalCode=employee.PostalCode,
                Country=employee.Country
            };
        }

        public static Employee EmployeeFromFullEmployeeFormDTO(this FullEmployeeFormDTO employee) 
        {
            return new Employee()
            {
                //AddressId=employee.AddressId,
                EmployeeType=employee.EmployeeType,
                StartDate=employee.StartDate,
                EndDate=employee.EndDate,
                //UserId=employee.UserId,
            };
        }

        public static User UserFromFullEmployeeFormDTO(this FullEmployeeFormDTO employee) 
        {
            return new User()
            {
                FirstName=employee.FirstName,
                LastName=employee.LastName,
                Email=employee.Email,
                Password=employee.Password,
            };
        }

        public static EmployeeFormDTO FromFullEmployeeToEmployeeFormDTO(this FullEmployeeFormDTO employee) 
        {
            return new EmployeeFormDTO()
            {
                //AddressId=employee.AddressId,
                EmployeeType=employee.EmployeeType,
                StartDate=employee.StartDate,
                EndDate=employee.EndDate,
                //UserId=employee.UserId,
            };
        }
    }
}

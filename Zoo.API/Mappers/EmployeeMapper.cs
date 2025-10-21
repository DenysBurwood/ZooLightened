using Zoo.API.DTOs;
using Zoo.DL.Entities.Humans;

namespace Zoo.API.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeAccountDTO ToEmployeeAccountDTO(this Employee employee) 
        {
            return new EmployeeAccountDTO()
            {
                FirstName=employee.User.FirstName,
                LastName=employee.User.LastName,
                Email=employee.User.Email,
                Address=employee.Address,
                StartDate=employee.StartDate,
                EndDate=employee.EndDate,
                EmployeeType=employee.EmployeeType,
            };
        }

    }
}

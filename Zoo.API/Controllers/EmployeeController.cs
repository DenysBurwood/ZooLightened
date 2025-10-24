using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zoo.API.DTOs.Employees;
using Zoo.API.Mappers;
using Zoo.API.Services;
using Zoo.API.Tools;
using Zoo.BLL.Exceptions;
using Zoo.BLL.Services;
using Zoo.DL.Entities;
using Zoo.DL.Entities.Humans;

namespace Zoo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController:ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;
        private readonly AddressService _addressService;
        private readonly EmployeeService _employeeService;
        public EmployeeController(UserService userService,AuthService authService,EmployeeService employeeService, AddressService addressService)
        {
            _userService=userService;
            _authService=authService;
            _employeeService=employeeService;
            _addressService=addressService;
        }

        [Authorize(Roles = $"Veterinarian,Administration,Director,Guide,Treasurer,Other,Admin")]
        [HttpGet("MyEmployeeSheet")]
        public ActionResult<EmployeeAccountDTO> MyEmployeeAccount()
        {
            User user = _userService.GetAccount(User.GetUserEmail());
            Address address = new Address();
            if(user.EmployeeId is null)
            {
                throw new UserNotFoundException("No Employee found");
            }
            Employee employee = _employeeService.GetEmployeeByEmployeeId(user.EmployeeId.Value)!;
            address.Id=employee.AddressId;
            address=_addressService.GetAddress(address.Id);
            EmployeeAccountDTO displayedEmployee = employee.ToEmployeeAccountDTO(user, address);
            return Ok(displayedEmployee);
        }

        [Authorize(Roles = "Administration,Director,Admin")]
        [HttpPost("NewEmployeeSheet")]
        public ActionResult EmployeeSheet([FromForm] EmployeeFormDTO employee)
        {
            if(employee is null||!ModelState.IsValid)
            {
                throw new RegisterException("Form not valid. Creation of employee aborted.");
            }
            User? user = _userService.GetAccount(employee.UserEmail);
            if(user is null) 
            {
                throw new UserNotFoundException($"No user with email:{employee.UserEmail} was found. Use instead the \"New employee\" form.");
            }
            if(user.EmployeeId is not null) 
            {
                throw new NotAllowedException("This user already works in the zoo.");
            }
            Employee newEmployee = _employeeService.CreateEmployeeSheet(employee.FromEmployeeFormDTO());
            _userService.SetEmployeeId(user,newEmployee.Id);
            return Created();
        }


        [Authorize(Roles = "Administration,Director,Admin")]
        [HttpPost("NewEmployee")]
        public ActionResult<EmployeeFormDTO> NewEmployee([FromForm] FullEmployeeFormDTO employee) 
        {
            if(employee is null||!ModelState.IsValid) 
            {
                throw new RegisterException("Form not valid. Creation of employee aborted.");
            }
            Address addressTemp = employee.AddressFromFullEmployeeFormDTO();
            User userTemp = employee.UserFromFullEmployeeFormDTO();
            Employee employeeTemp = employee.EmployeeFromFullEmployeeFormDTO();
            _addressService.CreateAddress(addressTemp);
            _userService.Register(userTemp);
            employeeTemp.UserId=userTemp.Id;
            employeeTemp.User=userTemp;
            employeeTemp.AddressId=addressTemp.Id;
            employeeTemp.Id=_employeeService.CreateEmployeeSheet(employeeTemp).Id;
            _userService.SetEmployeeId(userTemp,employeeTemp.Id);
            return Created();
        }

        [Authorize(Roles = "Director,Admin")]
        [HttpPost("FireEmployee/{employeeId:int}")]
        public ActionResult<EmployeeFormDTO> FireEmployee([FromRoute] int employeeId) 
        {
            Employee? employee = _employeeService.GetEmployeeByEmployeeId(employeeId);
            if(employee is null) 
            {
                throw new EmployeeNotFound($"No employee found with id: {employeeId}.");
            }
            User? user = _userService.GetUser(employee.UserId);
            if(user is null) 
            {
                throw new UserNotFoundException("An employee with no user account was found with employeeId: {employeeId}. Please fix this problem quickly.");
            }
            //employee.User=user;
            _employeeService.FireEmployee(employee, employeeId);
            _userService.UnSetEmployeeId(user, employeeId);
            return NoContent();
        }

    }
}

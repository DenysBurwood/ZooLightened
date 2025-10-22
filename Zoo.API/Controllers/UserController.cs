using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zoo.API.DTOs.Employees;
using Zoo.API.DTOs.Users;
using Zoo.API.Mappers;
using Zoo.API.Services;
using Zoo.API.Tools;
using Zoo.BLL.Exceptions;
using Zoo.BLL.Services;
using Zoo.DL.Entities.Humans;
using Zoo.DL.Enum;

namespace Zoo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;
        private readonly EmployeeService _employeeService;
        public UserController(UserService userService,AuthService authService,EmployeeService employeeService)
        {
            _userService=userService;
            _authService=authService;
            _employeeService=employeeService;
        }

        //  Later the index of animals will be available

        [HttpPost("Register")]
        public ActionResult Register([FromBody] UserFormDTO user)
        {
            if(user is null||!ModelState.IsValid)
            {
                throw new RegisterException("A valid form is required");
            }
            _userService.Register(user.FromUserForm());
            //Console.WriteLine(User.GetUserID());
            return Created();
        }

        [HttpPost("Login")]
        public ActionResult Login([FromBody] UserLoginDTO userLogin)
        {
            if(userLogin is null||!ModelState.IsValid)
            {
                return BadRequest();
            }
            User user = _userService.Login(userLogin.Email,userLogin.Password);
            int id = 0;
            if(user.EmployeeId is not null)
            {
                id=user.EmployeeId.Value;
            }
            Employee? employee = _employeeService.GetEmployeeByEmployeeId(id);
            string token = _authService.GenerateToken(user,employee);
            Console.WriteLine(token);
            return Ok(new { token });
        }

        [Authorize]
        [HttpPut("Subscription")]
        public ActionResult Subscribe()
        {

            int id = User.GetUserID();
            _userService.Subscribe(id);
            return Ok();
        }

        [Authorize]
        [HttpGet("MyAccount")]
        public ActionResult<UserAccountDTO> MyAccount()
        {
            string email = User.GetUserEmail();
            UserAccountDTO user = _userService.GetAccount(email).ToUserAccountDTO();
            return Ok(user);
        }

        [Authorize(Roles = $"Veterinarian,Administration,Director,Guide,Treasurer,Other,Admin")]
        [HttpGet("MyAccount/MyEmployeeSheet")]
        public ActionResult<EmployeeAccountDTO> MyEmployeeAccount() 
        {
            User user = _userService.GetAccount(User.GetUserEmail());
            if(user.EmployeeId is null)
            {
                throw new UserNotFoundException("No Employee found");
            }
            Employee employee = _employeeService.GetEmployeeByEmployeeId(user.EmployeeId.Value)!;
            employee.User=user;
            return Ok(employee.ToEmployeeAccountDTO());
        }

        [Authorize]
        [HttpPost("MyAccount")]
        public ActionResult<UserEditFormDTO> MyAccount([FromForm] UserEditFormDTO user)
        {
            if(user is null||!ModelState.IsValid) 
            {
                throw new RegisterException("Incomplete informations to fill edit the account.");
            }
            int id=User.GetUserID();
            _userService.EditAccount(user.FromUserEditForm(), id);
            return Ok();
        }

        [Authorize]
        [HttpGet("Delete")]
        public ActionResult Delete() 
        {
            if(!User.GetRole().Equals("Client")) 
            {
                throw new NotAllowedException("An employee cannot delete its user account");
            }

            _userService.Delete(_userService.GetAccount(User.GetUserEmail()));
            return Ok();
        }
    }
}

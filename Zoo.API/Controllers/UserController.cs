using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zoo.API.DTOs.Users;
using Zoo.API.Mappers;
using Zoo.API.Services;
using Zoo.API.Tools;
using Zoo.BLL.Exceptions;
using Zoo.BLL.Services;
using Zoo.DL.Entities.Humans;

namespace Zoo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;
        private readonly EmployeeService _employeeService;
        private readonly AddressService _addressService;
        public UserController(UserService userService,AuthService authService,EmployeeService employeeService,AddressService addressService)
        {
            _userService=userService;
            _authService=authService;
            _employeeService=employeeService;
            _addressService=addressService;
        }

        [HttpPost("Register")]
        public ActionResult Register([FromBody] UserFormDTO user)
        {
            if(user is null||!ModelState.IsValid)
            {
                throw new RegisterException("A valid form is required");
            }
            _userService.Register(user.FromUserForm());
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
            string email = _userService.GetUser(User.GetUserID())!.Email;
            UserAccountDTO user = _userService.GetAccount(email).ToUserAccountDTO();
            return Ok(user);
        }

        [Authorize]
        [HttpPut("EditAccount")]
        public ActionResult<UserEditFormDTO> MyAccount([FromBody] UserFormDTO user)
        {
            if(user is null||!ModelState.IsValid) 
            {
                throw new RegisterException("Incomplete informations to fill edit the account.");
            }
            int id=_userService.GetAccount(user.Email).Id;
            _userService.EditAccount(user.FromUserForm(), id);
            Employee? employee = _employeeService.GetEmployeeByEmployeeId(id);
            string token = _authService.GenerateToken(user.FromUserForm(),employee);
            Console.WriteLine(token);
            return Ok(new { token });
        }

        [Authorize]
        [HttpDelete("Delete")]
        public ActionResult Delete() 
        {
            if(!User.GetRole().Equals("Client")) 
            {
                throw new NotAllowedException("An employee cannot delete its user account");
            }
            string email = _userService.GetUser(User.GetUserID())!.Email;
            _userService.Delete(_userService.GetAccount(email));
            return Ok();
        }
    }
}

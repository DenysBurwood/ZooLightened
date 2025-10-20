using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zoo.API.DTOs;
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
        public UserController(UserService userService, AuthService authService) 
        {
            _userService = userService;
            _authService = authService;
        }

        //  Later the index of animals will be available

        [HttpPost("Register")]
        public ActionResult Register([FromBody] UserFormDTO user) 
        {
            if(user is null || !ModelState.IsValid) 
            {
                throw new RegisterException("A valid form is required");
            }
            _userService.Register(user.FromUserForm());
            Console.WriteLine(User.GetUserID());
            return Ok();
        }

        [HttpPost("Login")]
        public ActionResult Login([FromBody] UserLoginDTO userLogin) 
        {
            if (userLogin is null||!ModelState.IsValid) 
            {
                return BadRequest();
            }
            User user = _userService.Login(userLogin.Email, userLogin.Password);
            string token = _authService.GenerateToken(user);
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


    }
}

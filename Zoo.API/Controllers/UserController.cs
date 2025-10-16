using Microsoft.AspNetCore.Mvc;
using Zoo.API.DTOs;
using Zoo.API.Mappers;
using Zoo.API.Services;
using Zoo.BLL.Services;
using Zoo.DL.Entities.Humans;

namespace Zoo.API.Controllers
{
    [Route("api/User/[controller]")]
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
            _userService.Register(user.FromUserForm());
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
            return Ok(new { token });
        }

        [HttpPut("Subscription")]
        public ActionResult Subscribe([FromQuery] int id) 
        {
            _userService.Subscribe(id);
            return Ok();
        }


    }
}

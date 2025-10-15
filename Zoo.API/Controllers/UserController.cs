using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zoo.API.DTOs;
using Zoo.API.Mappers;
using Zoo.API.Services;
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
            //if(user is null||!ModelState.IsValid) 
            //{
            //    return BadRequest();
            //}
            ////  Hash of password
            //user.Password=Argon2.Hash(user.Password);

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
            //  if password wrong || user is null <- exception
            //if(user is null) 
            //{
            //    throw new LoginException(400,"Bad login or password.");
            //}
            //if(!Argon2.Verify(user.Password,userLogin.Password)) 
            //{
            //    throw new LoginException("Bad login or password.");
            //}
            string token = _authService.GenerateToken(user);
            return Ok(new { token });
        }


    }
}

using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

      

        [HttpPost("register")]
        public IActionResult RegisterIU(UserForRegisterDto UserForRegisterDto)
        {
            var userexists = _authService.UserExists(UserForRegisterDto.Email);
            if (!userexists.Success)
            {
                return BadRequest(userexists.Message);
            }

            var registerResult = _authService.Register(UserForRegisterDto, UserForRegisterDto.Password);
            var result = _authService.CreateAccessTokenForUser(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            return Ok(userToLogin);
            

            
        }
        
    }
}

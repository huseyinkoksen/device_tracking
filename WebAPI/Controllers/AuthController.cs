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

        [HttpPost("registercorporate")]
        public IActionResult RegisterCU(CorporateUserForRegisterDto corporateUserForRegisterDto)
        {
            var userexists = _authService.UserExists(corporateUserForRegisterDto.Email);
            if (!userexists.Success)
            {
                return BadRequest(userexists.Message);
            }

            var registerResult = _authService.RegisterCorporate(corporateUserForRegisterDto, corporateUserForRegisterDto.Password);
            var result = _authService.CreateAccessTokenForUser(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("registerindividual")]
        public IActionResult RegisterIU(IndividualUserForRegisterDto individualUserForRegisterDto)
        {
            var userexists = _authService.UserExists(individualUserForRegisterDto.Email);
            if (!userexists.Success)
            {
                return BadRequest(userexists.Message);
            }

            var registerResult = _authService.RegisterIndividual(individualUserForRegisterDto, individualUserForRegisterDto.Password);
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

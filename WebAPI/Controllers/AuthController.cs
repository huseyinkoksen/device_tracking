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
            var result = _authService.CreateAccessTokenForCorporateUser(registerResult.Data);
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
            var result = _authService.CreateAccessTokenForIndividualUser(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("logincorporate")]
        public ActionResult LoginCorporate(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.LoginCorporate(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessTokenForCorporateUser(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
        [HttpPost("loginindividual")]
        public ActionResult LoginIndividual(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.LoginIndividual(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessTokenForIndividualUser(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}

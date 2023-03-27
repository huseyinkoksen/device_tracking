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
        public IActionResult Register(CorporateUserForRegisterDto corporateUserForRegisterDto)
        {
            //var userExists = _authService.UserExists(corporateUserForRegisterDto.Email);
            //if (!userExists.Success)
            //{
            //    return BadRequest(userExists.Message);
            //}

            var registerResult = _authService.RegisterCorporate(corporateUserForRegisterDto, corporateUserForRegisterDto.Password);
            var result = _authService.CreateAccessTokenForCorporateUser(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}

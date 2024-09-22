using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _userService.GetById(id);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("changepassword")]
        public IActionResult ChangePassword(UpdatePasswordDto updatePasswordDto)
        {

            var result = _userService.ChangePassword(updatePasswordDto);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public IActionResult UpdateImage([FromForm] AddUserImageDto addUserImageDto)
        {
            var result = _userService.AddImage(addUserImageDto);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("[action]")]
        public IActionResult DeleteImage([FromForm] DeleteUserImageDto deleteUserImageDto)
        {
            var result = _userService.DeleteImage(deleteUserImageDto);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

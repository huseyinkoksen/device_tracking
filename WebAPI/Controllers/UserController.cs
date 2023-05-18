using Business.Abstract;
using Core.Utilities.Results;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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

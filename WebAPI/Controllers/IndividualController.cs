using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndividualController : ControllerBase
    {
        IUserService<IndividualUser> _userService;

        public IndividualController(IUserService<IndividualUser> userService)
        {
            _userService = userService;
        }

    }
}

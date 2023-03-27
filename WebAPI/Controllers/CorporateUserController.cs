using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorporateUserController : ControllerBase
    {
        IUserService<CorporateUser> _corporateUserService;

        public CorporateUserController(IUserService<CorporateUser> corporateUserService)
        {
            _corporateUserService = corporateUserService;
        }

        [HttpPost("add")]
        public IActionResult Add(CorporateUser entity)
        {
            var result= _corporateUserService.Add(entity);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

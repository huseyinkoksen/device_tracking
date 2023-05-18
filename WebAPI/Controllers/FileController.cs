using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public FileController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("[action]")]
        public IActionResult GetBaseUrl()
        {
            var url = _configuration.GetSection("BaseUrl").Value;
            return Ok(new { Url = url });
        }
    }
}

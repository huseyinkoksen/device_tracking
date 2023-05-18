using Business.Services.HttpContextAccessorService.Abstracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Business.Services.HttpContextAccessorService.Concretes
{
    public class HttpContextAccessorManager : IHttpContextAccessorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextAccessorManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentNameIdentifier()
        {
            return (_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value)!;
        }
    }
}

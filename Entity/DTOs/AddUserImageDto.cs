using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Entities.DTOs
{
    public class AddUserImageDto : IDto
    {
        public int Id { get; set; }
        public IFormFile FormFile { get; set; }
    }
}

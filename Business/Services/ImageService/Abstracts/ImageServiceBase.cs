using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Business.Services.ImageService.Abstracts
{
    public abstract class ImageServiceBase
    {
        private readonly IList<string> extensions = new List<string> { ".jpg", ".png", ".jpeg" };
        public abstract string Upload(IFormFile formFile);
        public string Update(string oldPath, IFormFile formFile)
        {
            Delete(oldPath);
            return Upload(formFile);
        }
        public abstract void Delete(string path);

        protected IResult MustBeImageFormat(IFormFile formFile)
        {
            string extension = Path.GetExtension(formFile.FileName);
            bool isHave = extensions.Contains(extension);
            if (!isHave) return new ErrorResult("Dosya resim formatında olmalı");

            return new SuccessResult();
        }
    }
}


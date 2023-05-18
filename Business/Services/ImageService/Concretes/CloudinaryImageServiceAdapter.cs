using Business.Services.ImageService.Abstracts;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Business.Services.ImageService.Concretes
{
    public class CloudinaryImageServiceAdapter : ImageServiceBase
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryImageServiceAdapter(IConfiguration configuration)
        {
            Account? account = configuration.GetSection("CloudinaryAccount").Get<CloudinaryDotNet.Account>();
            _cloudinary = new(account);
        }

        public override string Upload(IFormFile formFile)
        {
            //MustBeImageFormat(formFile);
            var uploadParams = new ImageUploadParams()
            {
                File = new(formFile.FileName, formFile.OpenReadStream()),
                UseFilename = false,
                UniqueFilename = true,
                Overwrite = false
            };
            var uploadResult = _cloudinary.Upload(uploadParams);
            return uploadResult.PublicId;
        }

        public override void Delete(string path)
        {
            var deletionParams = new DeletionParams(path);
            _cloudinary.Destroy(deletionParams);
        }
    }
}

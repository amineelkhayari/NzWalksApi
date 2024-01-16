using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NzWalks.Api.Models.Domain;
using NzWalks.Api.Models.DTO.ImageDtos;
using NzWalks.Api.Repositories.ImageUploaderFile;
using System.IO;

namespace NzWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUpController : ControllerBase
    {
        private readonly IUploaderRepository uploaderRepository;

        public ImageUpController(IUploaderRepository uploaderRepository)
        {
            this.uploaderRepository = uploaderRepository;
        }


        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> ImageUploader([FromForm] ImageUploadRequestDto request)
        {
            ValidateImage(request);
            if (ModelState.IsValid)
            {
                // user repositoty call to upload image
                var imageDomain = new Image
                {
                    File = request.File,
                    FileExtention = Path.GetExtension(request.File.FileName),
                    FileSize = request.File.Length,
                    FileName = request.FileName,
                    Description = request.description,

                };
                await uploaderRepository.ImageUploader(imageDomain);

                return Ok(imageDomain);
            }

            return BadRequest(ModelState);

        }

        private void ValidateImage(ImageUploadRequestDto request)
        {
            var AllowedImageExtention = new string[] { ".png", ".jpg", ".jpeg" };

            if (!AllowedImageExtention.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("File", "Unsupported File extention");
            }
            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "Please Select Image Smaller Than 10 Mb");

            }
        }



    }
}

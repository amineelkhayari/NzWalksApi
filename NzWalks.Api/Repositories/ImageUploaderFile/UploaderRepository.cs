using Microsoft.EntityFrameworkCore;
using NzWalks.Api.Data;
using NzWalks.Api.Models.Domain;

namespace NzWalks.Api.Repositories.ImageUploaderFile
{
    public class UploaderRepository : IUploaderRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NzWalksDbContext nzWalksDbContext;

        public UploaderRepository(IWebHostEnvironment webHostEnvironment, 
            IHttpContextAccessor httpContextAccessor,
            NzWalksDbContext nzWalksDbContext
            )
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.nzWalksDbContext = nzWalksDbContext;
        }    
        public async Task<Image> ImageUploader(Image image)
        {
            var LocalFilePath = Path.Combine(
                webHostEnvironment.ContentRootPath,
                "Images",$"{image.FileName}{image.FileExtention}"
                );

            //upload image to local path
            using var stream = new FileStream(LocalFilePath,FileMode.Create);
            await image.File.CopyToAsync(stream);
            var urlFilePath = 
                $"{httpContextAccessor.HttpContext.Request.Scheme}" +
                $"://{httpContextAccessor.HttpContext.Request.Host}" +
                $"{httpContextAccessor.HttpContext.Request.PathBase}" +
                $"/Images/{image.FileName}{image.FileExtention}";

            image.FilePath = urlFilePath;

            await nzWalksDbContext.Images.AddAsync(image);
            await nzWalksDbContext.SaveChangesAsync();
            return image;
        
        }
    }
}

using NzWalks.Api.Models.Domain;

namespace NzWalks.Api.Repositories.ImageUploaderFile
{
    public interface IUploaderRepository
    {
        public Task<Image> ImageUploader(Image image);
    }
}

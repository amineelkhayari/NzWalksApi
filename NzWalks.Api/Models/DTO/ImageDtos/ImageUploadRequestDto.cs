using System.ComponentModel.DataAnnotations;

namespace NzWalks.Api.Models.DTO.ImageDtos
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }
        public string? description { get; set; }
        [Required]
        public string FileName { get; set; }
    }
}

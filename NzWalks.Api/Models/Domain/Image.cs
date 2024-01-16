using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace NzWalks.Api.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? Description { get; set; }
        public string FileExtention { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
    }
}

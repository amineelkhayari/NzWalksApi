using System.ComponentModel.DataAnnotations;

namespace NzWalks.Api.Models.DTO.RegionsDtos
{
    public class AddRegionRequestDto
    {

        [Required]
        [MinLength(3, ErrorMessage = "lenght more than 3 characters")]
        [MaxLength(5, ErrorMessage = "lenght Less than 5 characters")]
        public string Code { get; set; }
        [Required]
       // [RegularExpression("/(w+)/i",ErrorMessage = "start your name with ")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }

    }
}

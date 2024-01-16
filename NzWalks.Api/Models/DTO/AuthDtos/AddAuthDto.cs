using System.ComponentModel.DataAnnotations;

namespace NzWalks.Api.Models.DTO.AuthDtos
{
    public class AddAuthDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}

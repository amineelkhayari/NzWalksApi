using System.ComponentModel.DataAnnotations;

namespace NzWalks.Api.Models.DTO.AuthDtos
{
    public class LoginAuthDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

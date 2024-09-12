using Piggybank.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Piggybank.Shared.Dtos
{
    public class LoginDto
    {
        [Required]
        public string UserNameOrEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(ConfigConstants.PasswordMinLength)]
        public string Password { get; set; }
    }
}

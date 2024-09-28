using Piggybank.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Piggybank.Shared.Dtos
{
    public class RegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(ConfigConstants.PasswordMinLength)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = ErrorMessages.PasswordsMismatch)]
        public string ConfirmPassword { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}

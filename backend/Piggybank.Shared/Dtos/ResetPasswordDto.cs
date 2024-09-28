using Piggybank.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Piggybank.Shared.Dtos
{
    public class ResetPasswordDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        [MinLength(ConfigConstants.PasswordMinLength)]
        public string NewPassword { get; set; }
        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = ErrorMessages.PasswordsMismatch)]
        public string ConfirmNewPassword { get; set; }
    }
}
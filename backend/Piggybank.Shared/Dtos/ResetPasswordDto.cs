using Piggybank.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Piggybank.Shared.Dtos
{
    /// <summary>
    /// Data Transfer Object for resetting a user's password.
    /// </summary>
    public class ResetPasswordDto
    {
        /// <summary>
        /// The email address of the user requesting the password reset. This field is required.
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// The token used for validating the password reset request. This field is required.
        /// </summary>
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// The new password for the user. This field is required and must meet the minimum length specified in <see cref="ConfigConstants.PasswordMinLength"/>.
        /// </summary>
        [Required]
        [MinLength(ConfigConstants.PasswordMinLength)]
        public string NewPassword { get; set; }

        /// <summary>
        /// The confirmation of the new password. This field must match <see cref="NewPassword"/> and is required.
        /// </summary>
        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = ErrorMessages.PasswordsMismatch)]
        public string ConfirmNewPassword { get; set; }
    }
}

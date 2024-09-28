using Piggybank.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Piggybank.Shared.Dtos
{
    /// <summary>
    /// Data Transfer Object for changing a user's password.
    /// </summary>
    public class ChangePasswordDto
    {
        /// <summary>
        /// The user's current password. This field is required and must meet the minimum length specified in <see cref="ConfigConstants.PasswordMinLength"/>.
        /// </summary>
        [Required]
        [MinLength(ConfigConstants.PasswordMinLength)]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// The user's new password. This field is required and must meet the minimum length specified in <see cref="ConfigConstants.PasswordMinLength"/>.
        /// </summary>
        [Required]
        [MinLength(ConfigConstants.PasswordMinLength)]
        public string NewPassword { get; set; }

        /// <summary>
        /// The confirmation of the new password. This field is required and must match <see cref="NewPassword"/>. If the passwords do not match, an error message is displayed.
        /// </summary>
        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = ErrorMessages.PasswordsMismatch)]
        public string ConfirmNewPassword { get; set; }
    }
}

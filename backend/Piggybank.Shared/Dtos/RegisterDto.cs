using Piggybank.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Piggybank.Shared.Dtos
{
    /// <summary>
    /// Data Transfer Object for user registration.
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// The email address of the user. This field is required.
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// The user's password. This field is required and must meet the minimum length specified in <see cref="ConfigConstants.PasswordMinLength"/>.
        /// </summary>
        [Required]
        [MinLength(ConfigConstants.PasswordMinLength)]
        public string Password { get; set; }

        /// <summary>
        /// The confirmation of the password. This field is required and must match <see cref="Password"/>. If the passwords do not match, an error message is displayed.
        /// </summary>
        [Required]
        [Compare(nameof(Password), ErrorMessage = ErrorMessages.PasswordsMismatch)]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// The username of the user. This field is required.
        /// </summary>
        [Required]
        public string UserName { get; set; }
    }
}

using Piggybank.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Piggybank.Shared.Dtos
{
    /// <summary>
    /// Data Transfer Object for user login credentials.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// The user's username or email. This field is required.
        /// </summary>
        [Required]
        public string UserNameOrEmail { get; set; }

        /// <summary>
        /// The user's password. This field is required and must meet the minimum length specified in <see cref="ConfigConstants.PasswordMinLength"/>.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [MinLength(ConfigConstants.PasswordMinLength)]
        public string Password { get; set; }
    }
}

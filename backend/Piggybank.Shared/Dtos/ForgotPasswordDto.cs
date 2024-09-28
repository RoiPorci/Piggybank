using System.ComponentModel.DataAnnotations;

namespace Piggybank.Shared.Dtos
{
    /// <summary>
    /// Data Transfer Object for initiating a forgot password request.
    /// </summary>
    public class ForgotPasswordDto
    {
        /// <summary>
        /// The email address of the user requesting the password reset. This field is required.
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}

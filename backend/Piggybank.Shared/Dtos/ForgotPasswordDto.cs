using System.ComponentModel.DataAnnotations;

namespace Piggybank.Shared.Dtos
{
    public class ForgotPasswordDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
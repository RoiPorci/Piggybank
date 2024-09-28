using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Piggybank.Models
{
    /// <summary>
    /// Represents an application user, extending the <see cref="IdentityUser"/> class with additional metadata.
    /// </summary>
    public class AppUser : IdentityUser
    {
        /// <summary>
        /// Date and time of the user's last login.
        /// </summary>
        public DateTime? LastLoginAt { get; set; }

        /// <summary>
        /// Date and time when the user account was created. This field is required.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date and time when the user account was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}

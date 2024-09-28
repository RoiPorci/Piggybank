using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Piggybank.Models
{
    /// <summary>
    /// Represents an application role, extending the <see cref="IdentityRole"/> class with additional metadata.
    /// </summary>
    public class AppRole : IdentityRole
    {
        /// <summary>
        /// Date and time when the role was created. This field is required.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date and time when the role was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}

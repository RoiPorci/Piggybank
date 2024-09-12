using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Piggybank.Models
{
    public class AppRole : IdentityRole
    {
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

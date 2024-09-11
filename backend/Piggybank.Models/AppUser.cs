using System.ComponentModel.DataAnnotations;

namespace Piggybank.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

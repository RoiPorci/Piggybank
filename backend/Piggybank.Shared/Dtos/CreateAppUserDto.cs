using System.ComponentModel.DataAnnotations;

namespace Piggybank.Shared.Dtos
{
    public class CreateAppUserDto : RegisterDto
    {
        [Required]
        public IList<string> Roles { get; set; }
    }
}
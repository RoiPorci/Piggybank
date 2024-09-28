using System.ComponentModel.DataAnnotations;

namespace Piggybank.Shared.Dtos
{
    public class UpdateAppUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }

    }

    public class UpdateAppUserWithRolesDto : UpdateAppUserDto
    {
        public List<string> Roles { get; set; }
    }
}
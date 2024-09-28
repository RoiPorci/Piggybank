using System.ComponentModel.DataAnnotations;

namespace Piggybank.Shared.Dtos
{
    /// <summary>
    /// Data Transfer Object for creating a new user with roles.
    /// Inherits from <see cref="RegisterDto"/> to include user registration details.
    /// </summary>
    public class CreateAppUserDto : RegisterDto
    {
        /// <summary>
        /// A list of roles to assign to the newly created user. This field is required.
        /// </summary>
        [Required]
        public IList<string> Roles { get; set; }
    }
}

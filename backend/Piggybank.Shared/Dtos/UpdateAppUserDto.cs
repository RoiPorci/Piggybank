using System.ComponentModel.DataAnnotations;

namespace Piggybank.Shared.Dtos
{
    /// <summary>
    /// Data Transfer Object for updating a user's basic information.
    /// </summary>
    public class UpdateAppUserDto
    {
        /// <summary>
        /// The user's username. This field is required.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// The user's email address. This field is required.
        /// </summary>
        [Required]
        public string Email { get; set; }
    }

    /// <summary>
    /// Data Transfer Object for updating a user's information, including roles.
    /// Inherits from <see cref="UpdateAppUserDto"/>.
    /// </summary>
    public class UpdateAppUserWithRolesDto : UpdateAppUserDto
    {
        /// <summary>
        /// List of roles assigned to the user.
        /// </summary>
        public List<string> Roles { get; set; }
    }
}

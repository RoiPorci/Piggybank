namespace Piggybank.Shared.Dtos
{
    /// <summary>
    /// Data Transfer Object for representing user information along with assigned roles.
    /// </summary>
    public class AppUserInfoDto
    {
        /// <summary>
        /// The unique identifier of the user.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// The username of the user.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// The email address of the user.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// A collection of roles assigned to the user.
        /// </summary>
        public IEnumerable<string> Roles { get; set; }
    }
}

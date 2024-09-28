namespace Piggybank.Shared.Dtos
{
    /// <summary>
    /// Data Transfer Object for representing detailed information about a user, including roles and last login time.
    /// </summary>
    public class AppUserDto
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
        /// Date and time of the user's last login.
        /// </summary>
        public DateTime? LastLoginAt { get; set; }

        /// <summary>
        /// A collection of roles assigned to the user.
        /// </summary>
        public IEnumerable<string> Roles { get; set; }
    }
}

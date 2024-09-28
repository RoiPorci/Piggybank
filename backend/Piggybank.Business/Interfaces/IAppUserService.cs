using Microsoft.AspNetCore.Identity;
using Piggybank.Models;
using Piggybank.Shared.Dtos;

namespace Piggybank.Business.Interfaces
{
    /// <summary>
    /// Service for managing AppUser entities, including operations related to user roles and identity management.
    /// </summary>
    public interface IAppUserService
    {
        /// <summary>
        /// Retrieves all users along with their roles.
        /// </summary>
        /// <returns>A collection of <see cref="AppUserDto"/> objects representing users and their roles.</returns>
        Task<IEnumerable<AppUserDto>> GetAllWithRolesAsync();

        /// <summary>
        /// Retrieves a user by their ID along with their roles.
        /// </summary>
        /// <param name="id">The user's ID as a string.</param>
        /// <returns>An <see cref="AppUserDto"/> representing the user with roles, or <see langword="null"/> if the user is not found.</returns>
        Task<AppUserDto?> GetByIdWithRolesAsync(string id);

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The user's ID as a string.</param>
        /// <returns>An <see cref="AppUser"/> object or <see langword="null"/> if the user is not found.</returns>
        Task<AppUser?> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves a user by their email or username.
        /// </summary>
        /// <param name="identifier">The user's email or username.</param>
        /// <returns>An <see cref="AppUser"/> object or <see langword="null"/> if the user is not found.</returns>
        Task<AppUser?> GetUserByIdentifierAsync(string identifier);

        /// <summary>
        /// Retrieves a user by their email.
        /// </summary>
        /// <param name="email">The user's email as a string.</param>
        /// <returns>An <see cref="AppUser"/> object or <see langword="null"/> if the user is not found.</returns>
        Task<AppUser?> GetByEmailAsync(string email);

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="userName">The user's username as a string.</param>
        /// <returns>An <see cref="AppUser"/> object or <see langword="null"/> if the user is not found.</returns>
        Task<AppUser?> GetByUserNameAsync(string userName);

        /// <summary>
        /// Retrieves the roles for a given user.
        /// </summary>
        /// <param name="user">The <see cref="AppUser"/> object.</param>
        /// <returns>A collection of <see cref="string"/> representing the user's roles.</returns>
        Task<IEnumerable<string>> GetRolesAsync(AppUser user);

        /// <summary>
        /// Adds a new user along with their roles.
        /// </summary>
        /// <param name="user">The <see cref="AppUser"/> object.</param>
        /// <param name="password">The password for the user as a <see cref="string"/>.</param>
        /// <param name="roles">A collection of roles to assign to the user as <see cref="IList{T}"/>.</param>
        /// <returns>An <see cref="IdentityResult"/> indicating the success or failure of the operation.</returns>
        Task<IdentityResult> AddAsync(AppUser user, string password, IList<string> roles);

        /// <summary>
        /// Increments the access failed count for a user.
        /// </summary>
        /// <param name="user">The <see cref="AppUser"/> object.</param>
        /// <returns>An <see cref="IdentityResult"/> indicating the success or failure of the update operation.</returns>
        Task<IdentityResult> FailAccessAsync(AppUser user);

        /// <summary>
        /// Updates a user and their roles.
        /// </summary>
        /// <param name="user">The <see cref="AppUser"/> object to update.</param>
        /// <param name="roles">A collection of new roles for the user as <see cref="IEnumerable{T}"/>.</param>
        /// <returns>An <see cref="IdentityResult"/> indicating the success or failure of the operation.</returns>
        Task<IdentityResult> UpdateAsync(AppUser user, IEnumerable<string> roles);

        /// <summary>
        /// Updates the last login timestamp for a user.
        /// </summary>
        /// <param name="user">The <see cref="AppUser"/> object.</param>
        /// <param name="lastLoginAt">The timestamp of the last login as <see cref="DateTime"/>. Defaults to the current time if <see langword="null"/>.</param>
        /// <returns>An <see cref="IdentityResult"/> indicating the success or failure of the update operation.</returns>
        Task<IdentityResult> UpdateLastLoginAsync(AppUser user, DateTime? lastLoginAt = null);

        /// <summary>
        /// Changes the user's password.
        /// </summary>
        /// <param name="user">The <see cref="AppUser"/> object.</param>
        /// <param name="currentPassword">The user's current password as a <see cref="string"/>.</param>
        /// <param name="newPassword">The new password to set as a <see cref="string"/>.</param>
        /// <returns>An <see cref="IdentityResult"/> indicating the success or failure of the operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the new password is <see langword="null"/> or empty.</exception>
        Task<IdentityResult> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword);

        /// <summary>
        /// Generates a password reset token for a user.
        /// </summary>
        /// <param name="user">The <see cref="AppUser"/> object.</param>
        /// <returns>A password reset token as a <see cref="string"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="user"/> is <see langword="null"/>.</exception>
        Task<string> GeneratePasswordResetTokenAsync(AppUser user);

        /// <summary>
        /// Resets the user's password using a reset token.
        /// </summary>
        /// <param name="user">The <see cref="AppUser"/> object.</param>
        /// <param name="token">The reset token as a <see cref="string"/>.</param>
        /// <param name="newPassword">The new password to set as a <see cref="string"/>.</param>
        /// <returns>An <see cref="IdentityResult"/> indicating the success or failure of the operation.</returns>
        Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword);

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The user's ID as a <see cref="string"/>.</param>
        /// <returns>An <see cref="IdentityResult"/> indicating the success or failure of the delete operation.</returns>
        Task<IdentityResult> DeleteAsync(string id);
    }
}

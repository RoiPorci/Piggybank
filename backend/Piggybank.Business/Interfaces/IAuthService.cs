using Microsoft.AspNetCore.Identity;
using Piggybank.Shared.Dtos;

namespace Piggybank.Business.Interfaces
{
    /// <summary>
    /// Provides authentication services including login, registration, token management, and user information retrieval.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Attempts to log in a user using the provided username or email and password.
        /// </summary>
        /// <param name="userNameOrEmail">The username or email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A <see cref="TokenDto"/> containing the user's token, or <see langword="null"/> if authentication fails.</returns>
        Task<TokenDto?> LoginAsync(string userNameOrEmail, string password);

        /// <summary>
        /// Registers a new user with the provided registration data.
        /// </summary>
        /// <param name="registerDto">An object containing user registration details.</param>
        /// <returns>An <see cref="IdentityResult"/> indicating success or failure of the operation.</returns>
        Task<IdentityResult> RegisterAsync(RegisterDto registerDto);

        /// <summary>
        /// Generates a new token for a user based on their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A <see cref="TokenDto"/> with the refreshed token, or <see langword="null"/> if the user is not found.</returns>
        Task<TokenDto?> RefreshTokenAsync(string userId);

        /// <summary>
        /// Retrieves user information, including roles, by user ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>An <see cref="AppUserDto"/> with user information, or <see langword="null"/> if the user is not found.</returns>
        Task<AppUserDto?> GetUserInfoByIdAsync(string userId);

        /// <summary>
        /// Logs out the user identified by the provided user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to log out.</param>
        /// <returns><see langword="void"/>.</returns>
        Task LogoutAsync(string userId);

        /// <summary>
        /// Updates user information for the specified user ID.
        /// </summary>
        /// <param name="updateDto">An object containing updated user information.</param>
        /// <param name="userId">The ID of the user to update.</param>
        /// <returns>An <see cref="IdentityResult"/> indicating the success or failure of the update.</returns>
        Task<IdentityResult> UpdateAsync(UpdateAppUserDto updateDto, string userId);

        /// <summary>
        /// Changes the user's password based on the provided data.
        /// </summary>
        /// <param name="changePasswordDto">An object containing current and new password details.</param>
        /// <param name="userId">The ID of the user to change the password for.</param>
        /// <returns>An <see cref="IdentityResult"/> indicating success or failure of the password change.</returns>
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto changePasswordDto, string userId);

        /// <summary>
        /// Resets a user's password using a token.
        /// </summary>
        /// <param name="userEmail">The email of the user whose password needs to be reset.</param>
        /// <param name="token">The token used for resetting the password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>An <see cref="IdentityResult"/> indicating the result of the password reset operation.</returns>
        Task<IdentityResult> ResetPasswordAsync(string userEmail, string token, string newPassword);

        /// <summary>
        /// Generates a password reset token for the user identified by the provided email.
        /// </summary>
        /// <param name="email">The email of the user requesting a password reset.</param>
        /// <returns>A string containing the password reset token, or <see langword="null"/> if the user is not found.</returns>
        Task<string?> GeneratePasswordResetTokenAsync(string email);

        /// <summary>
        /// Deletes a user identified by the provided user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>An <see cref="IdentityResult"/> indicating success or failure of the deletion.</returns>
        Task<IdentityResult> DeleteAsync(string userId);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Piggybank.Api.Contexts;
using Piggybank.Business.Interfaces;
using Piggybank.Shared.Constants;
using Piggybank.Shared.Dtos;
using System.Net;

namespace Piggybank.Api.Controller
{
    /// <summary>
    /// Provides endpoints for user authentication, registration, password management, and user information.
    /// </summary>
    [ApiController]
    [Route(RoutePatterns.ApiRoute)]
    public class AuthController : AppController
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">The authentication service used for managing user authentication.</param>
        /// <param name="logger">The logger instance for logging information.</param>
        /// <param name="userContext">The user context to retrieve the current user's ID.</param>
        public AuthController(IAuthService authService, ILogger<AuthController> logger, IUserContext userContext)
            : base(logger, userContext)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token.
        /// </summary>
        /// <param name="login">The login credentials (username or email and password).</param>
        /// <returns>
        /// A <see cref="TokenDto"/> containing the JWT token if authentication is successful, or an error if not.
        /// </returns>
        /// <response code="200">JWT token returned successfully.</response>
        /// <response code="401">Authentication failed.</response>
        [HttpPost(RoutePatterns.Login), ValidateModel]
        [ProducesResponseType(typeof(TokenDto), 200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto login)
        {
            TokenDto? token = await _authService.LoginAsync(login.UserNameOrEmail, login.Password);

            if (token == null)
                return Unauthenticated(ErrorMessages.LoginFailure);

            return Ok(token);
        }

        /// <summary>
        /// Registers a new user and returns a success message if the registration is successful.
        /// </summary>
        /// <param name="registerDto">The registration details (email, username, and password).</param>
        /// <returns>An OK result if registration is successful, or an error if not.</returns>
        /// <response code="200">User registered successfully.</response>
        /// <response code="400">Registration failed.</response>
        [HttpPost(RoutePatterns.Register), ValidateModel]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            IdentityResult result = await _authService.RegisterAsync(registerDto);

            if (!result.Succeeded)
                return Unauthenticated(result);

            return Ok(HttpMessages.UserCreationSuccess);
        }

        /// <summary>
        /// Retrieves the information of the currently authenticated user.
        /// </summary>
        /// <returns>The user information, including roles.</returns>
        /// <response code="200">User information retrieved successfully.</response>
        /// <response code="404">User not found.</response>
        [HttpGet(RoutePatterns.UserInfo)]
        [Authorize(Policy = ConfigConstants.UserPolicy)]
        [ProducesResponseType(typeof(AppUserDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<AppUserDto>> UserInfo()
        {
            AppUserDto? userInfo = await _authService.GetUserInfoByIdAsync(UserId);

            if (userInfo == null)
                return HandleError(ErrorMessages.UserNotFound, HttpMessages.UserNotFound);

            return Ok(userInfo);
        }

        /// <summary>
        /// Refreshes the JWT token for the authenticated user.
        /// </summary>
        /// <returns>A new <see cref="TokenDto"/> with the refreshed token.</returns>
        /// <response code="200">Token refreshed successfully.</response>
        /// <response code="401">Token refresh failed.</response>
        [HttpGet(RoutePatterns.RefreshToken)]
        [Authorize(Policy = ConfigConstants.UserPolicy)]
        [ProducesResponseType(typeof(TokenDto), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> RefreshToken()
        {
            TokenDto? token = await _authService.RefreshTokenAsync(UserId);

            if (token == null)
                return Unauthenticated(ErrorMessages.RegisterFailure);

            return Ok(token);
        }

        /// <summary>
        /// Updates the information of the currently authenticated user.
        /// </summary>
        /// <param name="updateDto">The updated user information.</param>
        /// <returns>An OK result if the update is successful, or an error if not.</returns>
        /// <response code="200">User information updated successfully.</response>
        /// <response code="400">User update failed.</response>
        [HttpPut(RoutePatterns.Update), ValidateModel]
        [Authorize(Policy = ConfigConstants.UserPolicy)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update([FromBody] UpdateAppUserDto updateDto)
        {
            IdentityResult result = await _authService.UpdateAsync(updateDto, UserId);

            if (!result.Succeeded)
                return HandleError(result, HttpMessages.UserUpdateFailure);

            return Ok(HttpMessages.UserUpdateSuccess);
        }

        /// <summary>
        /// Changes the password of the currently authenticated user.
        /// </summary>
        /// <param name="changePasswordDto">The current password and new password.</param>
        /// <returns>An OK result if the password change is successful, or an error if not.</returns>
        /// <response code="200">Password changed successfully.</response>
        /// <response code="400">Password change failed.</response>
        [HttpPatch(RoutePatterns.ChangePassword), ValidateModel]
        [Authorize(Policy = ConfigConstants.UserPolicy)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            IdentityResult result = await _authService.ChangePasswordAsync(changePasswordDto, UserId);

            if (!result.Succeeded)
                return HandleError(result, HttpMessages.ChangePasswordFailure);

            return Ok(HttpMessages.ChangePasswordSuccess);
        }

        /// <summary>
        /// Initiates the forgot password process by generating a password reset token.
        /// </summary>
        /// <param name="forgotPasswordDto">The email of the user requesting the password reset.</param>
        /// <returns>A <see cref="TokenDto"/> containing the password reset token, or an error if the user is not found.</returns>
        /// <response code="200">Password reset token generated successfully.</response>
        /// <response code="404">User not found.</response>
        [HttpPost(RoutePatterns.ForgotPassword), ValidateModel]
        [ProducesResponseType(typeof(TokenDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            string? token = await _authService.GeneratePasswordResetTokenAsync(forgotPasswordDto.Email);

            if (string.IsNullOrEmpty(token))
                return HandleError(ErrorMessages.UserNotFound, HttpMessages.UserNotFound);

            // TODO: Send the token via email
            return Ok(new TokenDto { Token = token });
        }

        /// <summary>
        /// Resets the user's password using a provided reset token and new password.
        /// </summary>
        /// <param name="resetPasswordDto">The email, reset token, and new password.</param>
        /// <returns>An OK result if the password reset is successful, or an error if not.</returns>
        /// <response code="200">Password reset successfully.</response>
        /// <response code="400">Password reset failed.</response>
        [HttpPost(RoutePatterns.ResetPassword), ValidateModel]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            IdentityResult result = await _authService.ResetPasswordAsync(resetPasswordDto.Email, resetPasswordDto.Token, resetPasswordDto.NewPassword);

            if (!result.Succeeded)
                return HandleError(result, HttpMessages.ResetPasswordFailure);

            return Ok(HttpMessages.ResetPasswordSuccess);
        }

        /// <summary>
        /// Logs out the currently authenticated user.
        /// </summary>
        /// <returns>An OK result indicating a successful logout.</returns>
        /// <response code="200">Logout successful.</response>
        [HttpPost(RoutePatterns.Logout)]
        [Authorize(Policy = ConfigConstants.UserPolicy)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync(UserId);

            return Ok(HttpMessages.LogoutSuccessful);
        }

        /// <summary>
        /// Deletes the account of the currently authenticated user.
        /// </summary>
        /// <returns>An OK result if the account deletion is successful, or an error if not.</returns>
        /// <response code="200">Account deleted successfully.</response>
        /// <response code="400">Account deletion failed.</response>
        [HttpDelete(RoutePatterns.Delete)]
        [Authorize(Policy = ConfigConstants.UserPolicy)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete()
        {
            IdentityResult result = await _authService.DeleteAsync(UserId);

            if (!result.Succeeded)
                return HandleError(result, HttpMessages.UserDeleteFailure);

            return Ok(HttpMessages.UserDeleteSuccess);
        }

        /// <summary>
        /// Handles unauthenticated access attempts by returning an error response.
        /// </summary>
        /// <param name="errorMessage">The error message to return.</param>
        /// <returns>An error response with an unauthorized status code.</returns>
        private ObjectResult Unauthenticated(string errorMessage)
        {
            return HandleError(errorMessage, HttpMessages.UserNotAuthenticated, HttpStatusCode.Unauthorized);
        }

        /// <summary>
        /// Handles unauthenticated access attempts by returning an error response.
        /// </summary>
        /// <param name="result">The result containing the error details.</param>
        /// <returns>An error response with an unauthorized status code.</returns>
        private ObjectResult Unauthenticated(IdentityResult result)
        {
            return HandleError(result, HttpMessages.UserNotAuthenticated, HttpStatusCode.Unauthorized);
        }
    }
}

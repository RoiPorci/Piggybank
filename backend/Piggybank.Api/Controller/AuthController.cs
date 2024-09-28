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
    [ApiController]
    [Route(RoutePatterns.ApiRoute)]
    public class AuthController : AppController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, ILogger<AuthController> logger, IUserContext userContext)
            : base(logger, userContext)
        {
            _authService = authService;
        }

        [HttpPost(RoutePatterns.Login), ValidateModel]
        public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto login)
        {
            TokenDto? token = await _authService.LoginAsync(login.UserNameOrEmail, login.Password);

            if (token == null)
                return Unauthenticated(ErrorMessages.LoginFailure);

            return Ok(token);
        }


        [HttpPost(RoutePatterns.Register), ValidateModel]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            IdentityResult result = await _authService.RegisterAsync(registerDto);

            if (!result.Succeeded)
                return Unauthenticated(result);

            return Ok(HttpMessages.UserCreationSuccess);
        }

        [HttpGet(RoutePatterns.UserInfo)]
        [Authorize(Policy = ConfigConstants.UserPolicy)]
        public async Task<ActionResult<AppUserDto>> UserInfo()
        {
            AppUserDto? userInfo = await _authService.GetUserInfoByIdAsync(UserId);

            if (userInfo == null)
                return HandleError(ErrorMessages.UserNotFound, HttpMessages.UserNotFound);

            return Ok(userInfo);
        }

        [HttpGet(RoutePatterns.RefreshToken)]
        [Authorize(Policy = ConfigConstants.UserPolicy)]
        public async Task<IActionResult> RefreshToken()
        {
            TokenDto? token = await _authService.RefreshTokenAsync(UserId);

            if (token == null)
                return Unauthenticated(ErrorMessages.RegisterFailure);

            return Ok(token);
        }

        [HttpPut(RoutePatterns.Update), ValidateModel]
        [Authorize(Policy = ConfigConstants.UserPolicy)]
        public async Task<IActionResult> Update([FromBody] UpdateAppUserDto updateDto)
        {
            IdentityResult result = await _authService.UpdateAsync(updateDto, UserId);

            if (!result.Succeeded)
                return HandleError(result, HttpMessages.UserUpdateFailure);

            return Ok(HttpMessages.UserUpdateSuccess);
        }
        
        [HttpPatch(RoutePatterns.ChangePassword), ValidateModel]
        [Authorize(Policy = ConfigConstants.UserPolicy)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            IdentityResult result = await _authService.ChangePasswordAsync(changePasswordDto, UserId);

            if (!result.Succeeded)
                return HandleError(result, HttpMessages.ChangePasswordFailure);

            return Ok(HttpMessages.ChangePasswordSuccess);
        }

        [HttpPost(RoutePatterns.ForgotPassword), ValidateModel]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            string? token = await _authService.GeneratePasswordResetTokenAsync(forgotPasswordDto.Email);

            if (string.IsNullOrEmpty(token))
                return HandleError(ErrorMessages.UserNotFound, HttpMessages.UserNotFound);

            //TODO: Send the token via email
            return Ok(new TokenDto { Token = token });
        }

        [HttpPost(RoutePatterns.ResetPassword), ValidateModel]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            IdentityResult result = await _authService.ResetPasswordAsync(resetPasswordDto.Email, resetPasswordDto.Token, resetPasswordDto.NewPassword);

            if (!result.Succeeded)
                return HandleError(result, HttpMessages.ResetPasswordFailure);

            return Ok(HttpMessages.ResetPasswordSuccess);
        }

        [HttpPost(RoutePatterns.Logout)]
        [Authorize(Policy = ConfigConstants.UserPolicy)]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync(UserId);

            return Ok(HttpMessages.LogoutSuccessful);
        }

        [HttpDelete(RoutePatterns.Delete)]
        [Authorize(Policy = ConfigConstants.UserPolicy)]
        public async Task<IActionResult> Delete()
        {
            IdentityResult result = await _authService.DeleteAsync(UserId);

            if (!result.Succeeded)
                return HandleError(result, HttpMessages.UserDeleteFailure);

            return Ok(HttpMessages.UserDeleteSuccess);
        }

        private ObjectResult Unauthenticated(string errorMessage)
        {
            return HandleError(errorMessage, HttpMessages.UserNotAuthenticated, HttpStatusCode.Unauthorized);
        }

        private ObjectResult Unauthenticated(IdentityResult result)
        {
            return HandleError(result, HttpMessages.UserNotAuthenticated, HttpStatusCode.Unauthorized);
        }
    }
}

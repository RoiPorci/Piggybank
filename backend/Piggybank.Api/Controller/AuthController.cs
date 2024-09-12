using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Piggybank.Business.Interfaces;
using Piggybank.Shared.Constants;
using Piggybank.Shared.Dtos;
using System.Security.Claims;

namespace Piggybank.Api.Controller
{
    [ApiController]
    [Route(RoutePatterns.ApiRoute)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost(RoutePatterns.Login)]
        public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation(ErrorMessages.LoginFailure);
                return BadRequest(ModelState);
            }

            TokenDto? token = await _authService.LoginAsync(login.UserNameOrEmail, login.Password);

            if (token == null)
            {
                _logger.LogInformation(ErrorMessages.LoginFailure);
                return Unauthorized(HttpMessages.InvalidCredentials);
            }

            return Ok(token);
        }

        [HttpPost(RoutePatterns.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation(ErrorMessages.RegisterFailure);
                return BadRequest(ModelState);
            }

            IdentityResult result = await _authService.RegisterAsync(registerDto);

            if (!result.Succeeded)
            {
                _logger.LogInformation(ErrorMessages.RegisterFailure);
                return BadRequest(result.Errors);
            }

            return Ok(HttpMessages.UserCreationSuccess);
        }

        [Authorize(Policy = ConfigConstants.UserPolicy)]
        [HttpGet(RoutePatterns.RefreshToken)]
        public async Task<IActionResult> RefreshToken()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogInformation(ErrorMessages.RefreshTokenFailure);
                return Unauthorized(HttpMessages.UserNotAuthenticated);
            }

            TokenDto? token = await _authService.RefreshTokenAsync(userId);

            if (token == null)
            {
                _logger.LogInformation(ErrorMessages.RefreshTokenFailure);
                return Unauthorized(HttpMessages.InvalidCredentials);
            }

            return Ok(token);
        }

        [HttpPost(RoutePatterns.Logout)]
        [Authorize(Policy = ConfigConstants.UserPolicy)]
        public async Task<IActionResult> Logout()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogInformation(ErrorMessages.LogoutFailure);
                return Unauthorized(HttpMessages.UserNotAuthenticated);
            }

            await _authService.LogoutAsync(userId);

            return Ok(HttpMessages.LogoutSuccessful);
        }
    }
}

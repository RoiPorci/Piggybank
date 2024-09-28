using Microsoft.AspNetCore.Identity;
using Piggybank.Business.Interfaces;
using Piggybank.Models;
using Piggybank.Shared.Constants;
using Piggybank.Shared.Dtos;

namespace Piggybank.Business.Services
{
    /// <summary>
    /// Implementation of <see cref="IAuthService"/> that provides authentication functionality using an external user service, token service and sign-in manager.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IAppUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="userService">The user service to manage user operations.</param>
        /// <param name="tokenService">The token service for managing JWT tokens.</param>
        /// <param name="signInManager">The sign-in manager to handle user sign-ins.</param>
        public AuthService(IAppUserService userService, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userService = userService;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        /// <inheritdoc />
        public async Task<TokenDto?> LoginAsync(string userNameOrEmail, string password)
        {
            AppUser? user = await _userService.GetUserByIdentifierAsync(userNameOrEmail);

            if (user == null)
                return null;

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (!result.Succeeded)
            {
                await _userService.FailAccessAsync(user);
                return null;
            }

            await _userService.UpdateLastLoginAsync(user);
            IEnumerable<string> roles = await _userService.GetRolesAsync(user);

            TokenDto token = _tokenService.GenerateJwtToken(user, roles);

            return token;
        }

        /// <inheritdoc />
        public async Task LogoutAsync(string userId)
        {
            AppUser? user = await _userService.GetByIdAsync(userId);
        }

        /// <inheritdoc />
        public async Task<AppUserDto?> GetUserInfoByIdAsync(string userId)
        {
            return await _userService.GetByIdWithRolesAsync(userId);
        }

        /// <inheritdoc />
        public async Task<TokenDto?> RefreshTokenAsync(string userId)
        {
            AppUser? user = await _userService.GetByIdAsync(userId);

            if (user == null)
                return null;

            IEnumerable<string> roles = await _userService.GetRolesAsync(user);

            TokenDto token = _tokenService.GenerateJwtToken(user, roles);

            return token;
        }

        /// <inheritdoc />
        public async Task<IdentityResult> RegisterAsync(RegisterDto registerDto)
        {
            AppUser user = new AppUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                CreatedAt = DateTime.UtcNow,
            };

            IdentityResult result = await _userService.AddAsync(user, registerDto.Password, new List<string> { ConfigConstants.UserRole });

            return result;
        }

        /// <inheritdoc />
        public async Task<IdentityResult> UpdateAsync(UpdateAppUserDto updateDto, string userId)
        {
            AppUser? user = await _userService.GetByIdAsync(userId);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = nameof(ErrorMessages.UserNotFound),
                    Description = ErrorMessages.UserNotFound
                });
            }

            user.UserName = updateDto.UserName;
            user.Email = updateDto.Email;

            return await _userService.UpdateAsync(user, new List<string>());
        }

        /// <inheritdoc />
        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto changePasswordDto, string userId)
        {
            AppUser? user = await _userService.GetByIdAsync(userId);

            if (user == null)
                return IdentityResult.Failed(new IdentityError
                {
                    Code = nameof(ErrorMessages.UserNotFound),
                    Description = ErrorMessages.UserNotFound
                });

            return await _userService.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
        }

        /// <inheritdoc />
        public async Task<string?> GeneratePasswordResetTokenAsync(string email)
        {
            AppUser? user = await _userService.GetByEmailAsync(email);

            if (user == null)
                return null;

            return await _userService.GeneratePasswordResetTokenAsync(user);
        }

        /// <inheritdoc />
        public async Task<IdentityResult> ResetPasswordAsync(string userEmail, string token, string newPassword)
        {
            AppUser? user = await _userService.GetByEmailAsync(userEmail);

            if (user == null)
                return IdentityResult.Failed(new IdentityError
                {
                    Code = nameof(ErrorMessages.UserNotFound),
                    Description = ErrorMessages.UserNotFound
                });

            return await _userService.ResetPasswordAsync(user, token, newPassword);
        }

        /// <inheritdoc />
        public async Task<IdentityResult> DeleteAsync(string userId)
        {
            return await _userService.DeleteAsync(userId);
        }
    }
}

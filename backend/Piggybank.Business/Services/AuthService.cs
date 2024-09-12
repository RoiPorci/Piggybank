using Microsoft.AspNetCore.Identity;
using Piggybank.Business.Interfaces;
using Piggybank.Models;
using Piggybank.Shared.Constants;
using Piggybank.Shared.Dtos;

namespace Piggybank.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAppUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(IAppUserService userService, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userService = userService;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

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

            await _userService.UpdateLastLogin(user);
            IEnumerable<string> roles = await _userService.GetRolesAsync(user);

            TokenDto token = _tokenService.GenerateJwtToken(user, roles);

            return token;
        }

        public async Task LogoutAsync(string userId)
        {
            AppUser? user = await _userService.GetByIdAsync(userId);
        }

        public async Task<TokenDto?> RefreshTokenAsync(string userId)
        {
            AppUser? user = await _userService.GetByIdAsync(userId);

            if (user == null)
                return null;

            IEnumerable<string> roles = await _userService.GetRolesAsync(user);

            TokenDto token = _tokenService.GenerateJwtToken(user, roles);

            return token;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto registerDto)
        {
            AppUser user = new AppUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                CreatedAt = DateTime.UtcNow,
            };

            IdentityResult result = await _userService.AddAsync(user, registerDto.Password, ConfigConstants.UserRole);

            return result;
        }
    }
}

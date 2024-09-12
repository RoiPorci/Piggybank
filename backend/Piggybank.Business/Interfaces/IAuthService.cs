using Microsoft.AspNetCore.Identity;
using Piggybank.Shared.Dtos;

namespace Piggybank.Business.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDto?> LoginAsync(string userNameOrEmail, string password);
        Task<IdentityResult> RegisterAsync(RegisterDto registerDto);
        Task<TokenDto?> RefreshTokenAsync(string userId);
        Task LogoutAsync(string userId);
    }
}

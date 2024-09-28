using Microsoft.AspNetCore.Identity;
using Piggybank.Shared.Dtos;

namespace Piggybank.Business.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDto?> LoginAsync(string userNameOrEmail, string password);
        Task<IdentityResult> RegisterAsync(RegisterDto registerDto);
        Task<TokenDto?> RefreshTokenAsync(string userId);
        Task<AppUserDto?> GetUserInfoByIdAsync(string userId);
        Task LogoutAsync(string userId);
        Task<IdentityResult> UpdateAsync(UpdateAppUserDto updateDto, string userId);
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto changePasswordDto, string userId);
        Task<IdentityResult> ResetPasswordAsync(string userEmail, string token, string newPassword);
        Task<string?> GeneratePasswordResetTokenAsync(string email);
        Task<IdentityResult> DeleteAsync(string userId);
    }
}

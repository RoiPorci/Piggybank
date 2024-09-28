using Microsoft.AspNetCore.Identity;
using Piggybank.Models;
using Piggybank.Shared.Dtos;

namespace Piggybank.Business.Interfaces
{
    public interface IAppUserService
    {
        Task<IEnumerable<AppUserDto>> GetAllWithRolesAsync();
        Task<AppUserDto?> GetByIdWithRolesAsync(string id);
        Task<AppUser?> GetByIdAsync(string id);
        Task<AppUser?> GetUserByIdentifierAsync(string identifier);
        Task<AppUser?> GetByEmailAsync(string email);
        Task<AppUser?> GetByUserNameAsync(string userName);
        Task<IEnumerable<string>> GetRolesAsync(AppUser user);
        Task<IdentityResult> AddAsync(AppUser user, string password, IList<string> roles);
        Task<IdentityResult> FailAccessAsync(AppUser user);
        Task<IdentityResult> UpdateLastLoginAsync(AppUser user, DateTime? lastLoginAt = null);
        Task<IdentityResult> UpdateAsync(AppUser user, IEnumerable<string> roles);
        Task<IdentityResult> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword);
        Task<string> GeneratePasswordResetTokenAsync(AppUser user);
        Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword);
        Task<IdentityResult> DeleteAsync(string id);
    }
}

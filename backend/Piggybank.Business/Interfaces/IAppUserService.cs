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
        Task<IdentityResult> AddAsync(AppUser user, string password, string role);
        Task<IdentityResult> FailAccessAsync(AppUser user);
        Task<IdentityResult> UpdateLastLogin(AppUser user, DateTime? lastLoginAt = null);
    }
}

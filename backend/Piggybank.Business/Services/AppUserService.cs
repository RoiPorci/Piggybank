using Microsoft.AspNetCore.Identity;
using Piggybank.Business.Interfaces;
using Piggybank.Data.Interfaces;
using Piggybank.Models;
using Piggybank.Shared.Dtos;

namespace Piggybank.Business.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;

        public AppUserService(IAppUserRepository userRepository, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<AppUserDto>> GetAllWithRolesAsync()
        {
            IEnumerable<AppUser> users = await _userRepository.GetAllAsync();

            IList<AppUserDto> userDtos = new List<AppUserDto>();

            foreach (AppUser user in users)
            {
                IEnumerable<string> roles = await _userManager.GetRolesAsync(user);

                userDtos.Add(new AppUserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    LastLoginAt = user.LastLoginAt,
                    Roles = roles
                });
            }

            return userDtos;
        }

        public async Task<AppUserDto?> GetByIdWithRolesAsync(string id)
        {
            AppUser? user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            IEnumerable<string> roles = await _userManager.GetRolesAsync(user);

            AppUserDto userDto = new AppUserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                LastLoginAt = user.LastLoginAt,
                Roles = roles
            };

            return userDto;
        }

        public async Task<AppUser?> GetByIdAsync(string id)
        {
            AppUser? user = await _userManager.FindByIdAsync(id);

            return user;
        }

        public async Task<AppUser?> GetUserByIdentifierAsync(string identifier)
        {
            return identifier.Contains("@")
                ? await GetByEmailAsync(identifier)
                : await GetByUserNameAsync(identifier);
        }

        public async Task<AppUser?> GetByEmailAsync(string email)
        {
            AppUser? user = await _userManager.FindByEmailAsync(email);

            return user;
        }

        public async Task<AppUser?> GetByUserNameAsync(string userName)
        {
            AppUser? user = await _userManager.FindByNameAsync(userName);

            return user;
        }

        public async Task<IEnumerable<string>> GetRolesAsync(AppUser user)
        {
            IEnumerable<string> roles = await _userManager.GetRolesAsync(user);

            return roles;
        }

        public async Task<IdentityResult> AddAsync(AppUser user, string password, string role)
        {
            IdentityResult result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return result;
            }

            result = await _userManager.AddToRoleAsync(user, role);

            return result;
        }

        public async Task<IdentityResult> FailAccessAsync(AppUser user)
        {
            user.AccessFailedCount++;
            IdentityResult result = await _userManager.UpdateAsync(user);

            return result;
        }

        public async Task<IdentityResult> UpdateLastLogin(AppUser user, DateTime? lastLoginAt = null)
        {
            if (lastLoginAt == null)
            {
                lastLoginAt = DateTime.UtcNow;
            }

            user.LastLoginAt = lastLoginAt;
            IdentityResult result = await _userManager.UpdateAsync(user);

            return result;
        }
    }
}

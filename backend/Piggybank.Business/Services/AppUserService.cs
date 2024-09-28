using Microsoft.AspNetCore.Identity;
using Piggybank.Business.Interfaces;
using Piggybank.Data.Interfaces;
using Piggybank.Models;
using Piggybank.Shared.Constants;
using Piggybank.Shared.Dtos;

namespace Piggybank.Business.Services
{
    /// <summary>
    /// Implementation of <see cref="IAppUserService"/> for managing users and their roles.
    /// </summary>
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// Constructor for initializing <see cref="AppUserService"/> with dependencies.
        /// </summary>
        /// <param name="userRepository">Repository for accessing user data.</param>
        /// <param name="userManager">Identity management service for <see cref="AppUser"/>.</param>
        public AppUserService(IAppUserRepository userRepository, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task<AppUserDto?> GetByIdWithRolesAsync(string id)
        {
            AppUser? user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            IEnumerable<string> roles = await _userManager.GetRolesAsync(user);

            return new AppUserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                LastLoginAt = user.LastLoginAt,
                Roles = roles
            };
        }

        /// <inheritdoc />
        public async Task<AppUser?> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        /// <inheritdoc />
        public async Task<AppUser?> GetUserByIdentifierAsync(string identifier)
        {
            return identifier.Contains("@")
                ? await GetByEmailAsync(identifier)
                : await GetByUserNameAsync(identifier);
        }

        /// <inheritdoc />
        public async Task<AppUser?> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        /// <inheritdoc />
        public async Task<AppUser?> GetByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<string>> GetRolesAsync(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        /// <inheritdoc />
        public async Task<IdentityResult> AddAsync(AppUser user, string password, IList<string> roles)
        {
            IdentityResult result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return result;

            return await _userManager.AddToRolesAsync(user, roles);
        }

        /// <inheritdoc />
        public async Task<IdentityResult> FailAccessAsync(AppUser user)
        {
            user.AccessFailedCount++;
            return await _userManager.UpdateAsync(user);
        }

        /// <inheritdoc />
        public async Task<IdentityResult> UpdateAsync(AppUser user, IEnumerable<string> roles)
        {
            user.UpdatedAt = DateTime.UtcNow;
            IdentityResult result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded || roles == null || !roles.Any())
                return result;

            IEnumerable<string> oldRoles = await _userManager.GetRolesAsync(user);
            result = await _userManager.RemoveFromRolesAsync(user, oldRoles);

            if (!result.Succeeded)
                return result;

            return await _userManager.AddToRolesAsync(user, roles);
        }

        /// <inheritdoc />
        public async Task<IdentityResult> UpdateLastLoginAsync(AppUser user, DateTime? lastLoginAt = null)
        {
            if (lastLoginAt == null)
                lastLoginAt = DateTime.UtcNow;

            user.LastLoginAt = lastLoginAt;
            return await _userManager.UpdateAsync(user);
        }

        /// <inheritdoc />
        public async Task<IdentityResult> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
                throw new ArgumentNullException(nameof(newPassword));

            IdentityResult result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (!result.Succeeded)
                return result;

            return await UpdateAsync(user, new List<string>());
        }

        /// <inheritdoc />
        public async Task<string> GeneratePasswordResetTokenAsync(AppUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        /// <inheritdoc />
        public async Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword)
        {
            if (user == null)
                return IdentityResult.Failed(new IdentityError
                {
                    Code = nameof(ErrorMessages.UserNotFound),
                    Description = ErrorMessages.UserNotFound
                });

            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        /// <inheritdoc />
        public async Task<IdentityResult> DeleteAsync(string id)
        {
            AppUser? user = await GetByIdAsync(id);

            if (user == null)
                return IdentityResult.Failed(new IdentityError
                {
                    Code = nameof(ErrorMessages.UserNotFound),
                    Description = ErrorMessages.UserNotFound
                });

            IList<string> roles = await _userManager.GetRolesAsync(user);
            IdentityResult result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
                return result;

            return await _userManager.DeleteAsync(user);
        }
    }
}

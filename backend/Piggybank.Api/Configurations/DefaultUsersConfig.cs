using Microsoft.AspNetCore.Identity;
using Piggybank.Models;
using Piggybank.Shared.Constants;

namespace Piggybank.Api.Configurations
{
    /// <summary>
    /// Provides methods for adding default admin and user accounts to the application.
    /// </summary>
    public static class DefaultUsersConfig
    {
        /// <summary>
        /// Adds the default admin user to the application if it does not already exist.
        /// </summary>
        /// <param name="app">The <see cref="WebApplication"/> instance.</param>
        public static async Task AddDefaultAdmin(this WebApplication app)
        {
            using (IServiceScope scope = app.Services.CreateScope())
            {
                UserManager<AppUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                AppUser? adminUser = await userManager.FindByEmailAsync(ConfigConstants.DefaultAdminEmail);

                if (adminUser == null)
                {
                    adminUser = new AppUser
                    {
                        UserName = ConfigConstants.DefaultAdminUserName,
                        Email = ConfigConstants.DefaultAdminEmail,
                        CreatedAt = DateTime.UtcNow,
                    };

                    IdentityResult? result = await userManager.CreateAsync(adminUser, ConfigConstants.DefaultAdminPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, ConfigConstants.AdminRole);
                    }
                }
            }
        }

        /// <summary>
        /// Adds the default regular user to the application if it does not already exist.
        /// </summary>
        /// <param name="app">The <see cref="WebApplication"/> instance.</param>
        public static async Task AddDefaultUser(this WebApplication app)
        {
            using (IServiceScope scope = app.Services.CreateScope())
            {
                UserManager<AppUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                AppUser? user = await userManager.FindByEmailAsync(ConfigConstants.DefaultUserEmail);

                if (user == null)
                {
                    user = new AppUser
                    {
                        UserName = ConfigConstants.DefaultUserUserName,
                        Email = ConfigConstants.DefaultUserEmail,
                        CreatedAt = DateTime.UtcNow,
                    };

                    IdentityResult? result = await userManager.CreateAsync(user, ConfigConstants.DefaultUserPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, ConfigConstants.UserRole);
                    }
                }
            }
        }
    }
}

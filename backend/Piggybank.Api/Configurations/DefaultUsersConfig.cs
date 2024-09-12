using Microsoft.AspNetCore.Identity;
using Piggybank.Models;
using Piggybank.Shared.Constants;

namespace Piggybank.Api.Configurations
{
    public static class DefaultUsersConfig
    {
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

        public static async Task AddDefaultUser(this WebApplication app)
        {
            using (IServiceScope scope = app.Services.CreateScope())
            {
                UserManager<AppUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                AppUser? adminUser = await userManager.FindByEmailAsync(ConfigConstants.DefaultUserEmail);

                if (adminUser == null)
                {
                    adminUser = new AppUser
                    {
                        UserName = ConfigConstants.DefaultUserUserName,
                        Email = ConfigConstants.DefaultUserEmail,
                        CreatedAt = DateTime.UtcNow,
                    };

                    IdentityResult? result = await userManager.CreateAsync(adminUser, ConfigConstants.DefaultUserPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, ConfigConstants.UserRole);
                    }
                }
            }
        }
    }
}

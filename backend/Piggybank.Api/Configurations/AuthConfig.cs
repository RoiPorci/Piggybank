using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Piggybank.Data;
using Piggybank.Models;
using Piggybank.Shared.Constants;
using System.Text;

namespace Piggybank.Api.Configurations
{
    public static class AuthConfig
    {
        public static void AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityAutorization();
            services.AddJwtAuthentication(configuration);
        }

        public static void AddIdentityAutorization(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ConfigConstants.AdminPolicy, policy => policy.RequireRole(ConfigConstants.AdminRole));
                options.AddPolicy(ConfigConstants.UserPolicy, policy => policy.RequireRole(ConfigConstants.UserRole, ConfigConstants.AdminRole));
            });
        }

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                string jwtSecret = configuration[ConfigConstants.JwtSecret]
                    ?? throw new InvalidOperationException(string.Format(ErrorMessages.JwtSecretNotFound, ConfigConstants.JwtSecret));

                // Configure JWT (ex: secret, validation)
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                    ValidIssuer = configuration[ConfigConstants.JwtIssuer],
                };
            });
        }

        public static async Task AddRoles(this WebApplication app)
        {
            await app.AddAdminRole();
            await app.AddUserRole();
        }

        public static async Task AddAdminRole(this WebApplication app)
        {
            using (IServiceScope scope = app.Services.CreateScope())
            {
                RoleManager<AppRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

                if (!await roleManager.RoleExistsAsync(ConfigConstants.AdminRole))
                {
                    AppRole adminRole = new AppRole
                    {
                        Name = ConfigConstants.AdminRole,
                        CreatedAt = DateTime.UtcNow,
                    };
                    await roleManager.CreateAsync(adminRole);
                }
            }
        }

        public static async Task AddUserRole(this WebApplication app)
        {
            using (IServiceScope scope = app.Services.CreateScope())
            {
                RoleManager<AppRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

                if (!await roleManager.RoleExistsAsync(ConfigConstants.UserRole))
                {
                    AppRole userRole = new AppRole
                    {
                        Name = ConfigConstants.UserRole,
                        CreatedAt = DateTime.UtcNow,
                    };
                    await roleManager.CreateAsync(userRole);
                }
            }
        }
    }
}

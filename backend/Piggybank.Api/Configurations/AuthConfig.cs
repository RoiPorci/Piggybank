using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Piggybank.Data;
using Piggybank.Models;
using Piggybank.Shared.Constants;
using System.Text;

namespace Piggybank.Api.Configurations
{
    /// <summary>
    /// Provides configuration methods for authentication and authorization services, including JWT and Identity.
    /// </summary>
    public static class AuthConfig
    {
        /// <summary>
        /// Configures authentication and authorization services for the application.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="configuration">The configuration to retrieve JWT settings.</param>
        public static void AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityAutorization();
            services.AddJwtAuthentication(configuration);
        }

        /// <summary>
        /// Configures Identity and authorization services, including role-based policies.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
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

        /// <summary>
        /// Configures JWT authentication services for the application, including token validation parameters.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="configuration">The configuration to retrieve JWT settings.</param>
        /// <exception cref="InvalidOperationException">Thrown if the JWT secret is not found in the configuration.</exception>
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                string jwtSecret = configuration.GetSection(ConfigConstants.JwtSection)[ConfigConstants.JwtSecret]
                    ?? throw new InvalidOperationException(string.Format(ErrorMessages.JwtSecretNotFound, ConfigConstants.JwtSecret));

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

        /// <summary>
        /// Adds the default roles (Admin and User) to the application if they do not already exist.
        /// </summary>
        /// <param name="app">The <see cref="WebApplication"/> instance.</param>
        public static async Task AddRoles(this WebApplication app)
        {
            await app.AddAdminRole();
            await app.AddUserRole();
        }

        /// <summary>
        /// Adds the Admin role to the application if it does not exist.
        /// </summary>
        /// <param name="app">The <see cref="WebApplication"/> instance.</param>
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

        /// <summary>
        /// Adds the User role to the application if it does not exist.
        /// </summary>
        /// <param name="app">The <see cref="WebApplication"/> instance.</param>
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

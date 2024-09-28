using Piggybank.Business.Interfaces;
using Piggybank.Business.Services;

namespace Piggybank.Api.Configurations
{
    /// <summary>
    /// Provides configuration methods for adding business services to the application's dependency injection container.
    /// </summary>
    public static class BusinessConfig
    {
        /// <summary>
        /// Registers business services for dependency injection, including user management, authentication, token management, and email services.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}

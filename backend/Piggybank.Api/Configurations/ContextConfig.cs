using Piggybank.Api.Contexts;

namespace Piggybank.Api.Configurations
{
    /// <summary>
    /// Provides configuration methods for adding context services to the application's dependency injection container.
    /// </summary>
    public static class ContextConfig
    {
        /// <summary>
        /// Registers the context services for dependency injection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        public static void AddContextsServices(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
        }
    }
}

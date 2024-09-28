namespace Piggybank.Api.Configurations
{
    /// <summary>
    /// Provides configuration methods to add various application services.
    /// </summary>
    public static class ServiceConfig
    {
        /// <summary>
        /// Configures and registers application services, including database, business logic, authentication, and Swagger/OpenAPI.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="configuration">The application's configuration to retrieve necessary settings.</param>
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseServices(configuration);
            services.AddBusinessServices();
            services.AddContextsServices();
            services.AddControllers();
            services.AddAuthServices(configuration);

            // Configures Swagger/OpenAPI services for API documentation.
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Piggybank.Data.Interfaces;
using Piggybank.Data.Repositories;
using Piggybank.Data;
using Piggybank.Shared.Constants;

namespace Piggybank.Api.Configurations
{
    /// <summary>
    /// Provides configuration methods for adding database services and repositories.
    /// </summary>
    public static class DatabaseConfig
    {
        /// <summary>
        /// Configures database services and repositories for the application.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="configuration">The configuration to retrieve connection strings and environment variables.</param>
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAppDbContext(configuration);
            services.AddRepositories();
        }

        /// <summary>
        /// Configures the application's database context using the provided connection string and environment variables.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="configuration">The configuration to retrieve connection strings and environment variables.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the connection string, environment variable name, or environment variable value is not found.
        /// </exception>
        public static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(ConfigConstants.DefaultConnectionString)
                ?? throw new InvalidOperationException(string.Format(ErrorMessages.ConnectionStringNotFound, ConfigConstants.DefaultConnectionString));

            string dbPasswordEnvVariable = configuration[ConfigConstants.DbPasswordEnvVariable]
                ?? throw new InvalidOperationException(string.Format(ErrorMessages.DbPasswordEnvVariableNotFound, ConfigConstants.DbPasswordEnvVariable));

            string dbPassword = Environment.GetEnvironmentVariable(dbPasswordEnvVariable)
                ?? throw new InvalidOperationException(string.Format(ErrorMessages.EnvironmentVariableNotFound, dbPasswordEnvVariable));

            connectionString = connectionString.Replace($"{ConfigConstants.DbPasswordInConnectionstring}", dbPassword);

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));
        }

        /// <summary>
        /// Registers the application's repositories for dependency injection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAppUserRepository, AppUserRepository>();
        }
    }
}

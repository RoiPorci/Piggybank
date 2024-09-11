using Microsoft.EntityFrameworkCore;
using Piggybank.Data;
using Piggybank.Data.Interfaces;
using Piggybank.Data.Repositories;
using Piggybank.Shared.Constants;

namespace Piggybank.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAppDbContext(configuration);
            services.AddRepositories();
        }

        public static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(ConfigConstants.DefaultConnectionString)
                ?? throw new InvalidOperationException(ErrorMessages.ConnectionStringNotFound);

            string dbPassword = Environment.GetEnvironmentVariable(ConfigConstants.DbPassword)
                ?? throw new InvalidOperationException(ErrorMessages.EnvironmentVariableDbPasswordNotFound);

            connectionString = connectionString.Replace($"%{ConfigConstants.DbPassword}%", dbPassword);

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAppUserRepository, AppUserRepository>();
        }
    }
}

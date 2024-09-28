namespace Piggybank.Api.Configurations
{
    public static class ServiceConfig
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseServices(configuration);
            services.AddBusinessServices();
            services.AddContextsServices();
            services.AddControllers();
            services.AddAuthServices(configuration);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}

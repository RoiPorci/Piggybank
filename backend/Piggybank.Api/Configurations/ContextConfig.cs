using Piggybank.Api.Contexts;

namespace Piggybank.Api.Configurations
{
    public static class ContextConfig
    {
        public static void AddContextsServices(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
        }
    }
}

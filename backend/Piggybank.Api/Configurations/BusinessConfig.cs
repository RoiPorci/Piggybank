using Piggybank.Business.Interfaces;
using Piggybank.Business.Services;

namespace Piggybank.Api.Configurations
{
    public static class BusinessConfig
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IAppUserService, AppUserService>();
        }
    }
}

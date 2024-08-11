using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tenancy.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddTenancy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITenantService, TenantService>();

            return services;
            
        }
    }
}

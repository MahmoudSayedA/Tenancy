using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Tenancy.Core.Services.Data;

namespace Tenancy.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IDbContext, ApplicationDbContext>();
            return services;
        }
    }
}

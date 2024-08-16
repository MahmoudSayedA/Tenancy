using Microsoft.EntityFrameworkCore;
using Tenancy.Infrastructure.Data;
using Tenancy.Infrastructure.Services;
using Tenancy.Infrastructure.Settings;

namespace Tenancy.WebApplication1.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTenancy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITenantService, TenantService>();

            services.Configure<TenantSettings>(configuration.GetSection(nameof(TenantSettings)));
            TenantSettings tenantSettings = new();
            configuration.GetSection(nameof(TenantSettings)).Bind(tenantSettings);
            
            var provider = tenantSettings.Defaults.DBProvider.ToLower();

            // Add the default connection string
            if (provider == "sqlserver")
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(tenantSettings.Defaults.ConnectionString));
            }
            
            // Add the tenant connection strings
            foreach(var tenant in tenantSettings.Tenants)
            {
                if (provider == "sqlserver")
                {
                    var conn = tenant.ConnectionString ?? tenantSettings.Defaults.ConnectionString;
                    using var scope = services.BuildServiceProvider().CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    context.Database.SetConnectionString(conn);

                    // Apply migrations if any
                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                    }

                }
            }


            return services;
        }

    }
}  

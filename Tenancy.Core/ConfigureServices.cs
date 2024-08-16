using Microsoft.Extensions.DependencyInjection;
using Tenancy.Core.Services.Reports;

namespace Tenancy.Core
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IReportService, ReportService>();
            return services;
        }
    }
}

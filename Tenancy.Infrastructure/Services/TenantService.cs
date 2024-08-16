
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Tenancy.Infrastructure.Services
{
    public class TenantService : ITenantService
    {
        private readonly TenantSettings _tenantSettings;
        private readonly HttpContext? _httpContext;
        private Tenant? _currentTenant;

        public TenantService(IOptions<TenantSettings> tenantSettings, IHttpContextAccessor httpContextAccessor) 
        {
            _tenantSettings = tenantSettings.Value;
            _httpContext = httpContextAccessor.HttpContext;
            if (_httpContext is not null)
            {
                // get tenant from header and check if matches with the tenant settings
                if (_httpContext.Request.Headers.TryGetValue("tenant", out var tenantId))
                {

                    _currentTenant = _tenantSettings.Tenants.FirstOrDefault(t => t.TenantId == tenantId)
                        ??throw new Exception(tenantId + " is not a valid tenant");

                    // set the connection string to the default if not provided
                    _currentTenant.ConnectionString ??= _tenantSettings.Defaults.ConnectionString;
                }
                else
                {
                    throw new Exception("tenant is not provided!");
                }
            }
        }

        public string? GetConnectionString()
        {
            return _currentTenant?.ConnectionString ?? _tenantSettings.Defaults.ConnectionString;
        }

        public Tenant? GetCurrentTenant()
        {
            return _currentTenant;
        }

        public string? GetProvider()
        {
            return _tenantSettings.Defaults.DBProvider;
        }
    }
}

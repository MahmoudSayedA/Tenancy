namespace Tenancy.Infrastructure.Services
{
    public interface ITenantService
    {
        string? GetProvider();
        string? GetConnectionString();
        Tenant? GetCurrentTenant();
    }
}

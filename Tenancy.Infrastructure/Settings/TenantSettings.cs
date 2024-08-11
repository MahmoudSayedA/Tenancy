namespace Tenancy.Infrastructure.Settings
{
    public class TenantSettings
    {
        public Configuration Defaults { get; set; } = null!;
        public List<Tenant> Tenants { get; set; } = new();

    }
}

namespace Tenancy.Core.Contracts
{
    public interface IHaveTenant
    {
        string TenantId { get; set; }
    }
}

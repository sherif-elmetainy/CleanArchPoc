namespace CodeArt.Poc.Core.Tenants;

public class Tenant : IEntity<TenantId>
{
    public TenantId Id { get; set; } = TenantId.New();
    
    public TenantName Name { get; set; }
}
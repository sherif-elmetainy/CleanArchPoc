
using CodeArt.Poc.Core.Tenants;

namespace CodeArt.Poc.TenantsApi.Tenants;

public class TenantRow
{
    public TenantId Id { get; set; }
    public TenantName Name { get; set; }
}
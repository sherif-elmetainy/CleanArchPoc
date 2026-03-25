using CodeArt.Poc.Core.Tenants;
using CodeArt.Poc.Storage.Common;

namespace CodeArt.Poc.WebApi;

public class CurrentTenantProvider : ICurrentTenantProvider
{
    public TenantName Name { get; } = TenantName.From("sherif");
    public TenantId Id { get; } = TenantId.New();
}
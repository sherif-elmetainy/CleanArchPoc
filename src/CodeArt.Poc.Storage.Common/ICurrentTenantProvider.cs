using TenantId = CodeArt.Poc.Core.Tenants.TenantId;
using TenantName = CodeArt.Poc.Core.Tenants.TenantName;

namespace CodeArt.Poc.Storage.Common;

public interface ICurrentTenantProvider
{
    TenantName Name { get; }
    TenantId Id { get; }
}
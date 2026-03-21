using CodeArt.Poc.Primitives;

namespace CodeArt.Poc.Infrastructure.Abstractions;

public interface ICurrentTenantProvider
{
    TenantName Name { get; }
    TenantId Id { get; }
}
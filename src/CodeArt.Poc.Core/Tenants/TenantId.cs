using System.Numerics;

using Vogen;

namespace CodeArt.Poc.Core.Tenants;

[ValueObject<Guid>]
public readonly partial struct TenantId : IEqualityOperators<TenantId, TenantId, bool>
{
    public static TenantId New() => From(Guid.CreateVersion7());
    
    public static TenantId SystemId { get; } = From(Guid.Parse("00000000-0000-0000-0000-000000000000"));
}
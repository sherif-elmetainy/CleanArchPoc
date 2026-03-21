using Vogen;

namespace CodeArt.Poc.Primitives;

[ValueObject<Guid>]
public readonly partial struct TenantId
{
    public static TenantId New() => From(Guid.CreateVersion7());
    
    public static TenantId SystemId { get; } = From(Guid.Parse("00000000-0000-0000-0000-000000000000"));
}
using System.Numerics;

namespace CodeArt.Poc.Infrastructure.Abstractions;

public interface IEntity<TId> where TId : IEquatable<TId>, IEqualityOperators<TId, TId, bool>
{
    public TId Id { get; set; }
}
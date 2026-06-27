using System.Numerics;

namespace CodeArt.Poc.Core;

public interface IRepository<TEntity, in TId>
    where TEntity : class, IEntity<TId>, new()
    where TId : IEquatable<TId>, IEqualityOperators<TId, TId, bool>
{
    IQueryable<TEntity> Query { get; }
    void Add(TEntity entity);
    void Delete(TEntity entity);
    void DeleteById(TId id);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
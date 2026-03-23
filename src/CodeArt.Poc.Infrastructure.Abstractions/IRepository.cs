using System.Numerics;

namespace CodeArt.Poc.Infrastructure.Abstractions;

public interface IRepository<TEntity, in TId>
    where TEntity : class, IEntity<TId>, new()
    where TId : IEquatable<TId>, IEqualityOperators<TId, TId, bool>
{
    public IQueryable<TEntity> GetQueryable();
    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    public Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    public ValueTask<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken);
    public Task<bool> DeleteById(TId id, CancellationToken cancellationToken);
}
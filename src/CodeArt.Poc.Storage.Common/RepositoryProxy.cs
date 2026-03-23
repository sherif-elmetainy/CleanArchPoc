using System.Numerics;

using CodeArt.Poc.Core;

namespace CodeArt.Poc.Storage.Common;

internal sealed class RepositoryProxy<TEntity, TId>(
    IServiceProvider serviceProvider,
    IRepositoryFactory<TEntity, TId> repositoryFactory)
    : IRepository<TEntity, TId>
    where TId : IEquatable<TId>, IEqualityOperators<TId, TId, bool>
    where TEntity : class, IEntity<TId>, new()
{
    private readonly IRepository<TEntity, TId> _repository = repositoryFactory.CreateRepository(serviceProvider);


    public IQueryable<TEntity> GetQueryable()
    {
        return _repository.GetQueryable();
    }

    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        return _repository.AddAsync(entity, cancellationToken);
    }

    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        return _repository.UpdateAsync(entity, cancellationToken);
    }

    public Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        return _repository.DeleteAsync(entity, cancellationToken);
    }

    public ValueTask<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        return _repository.GetByIdAsync(id, cancellationToken);
    }

    public Task<bool> DeleteById(TId id, CancellationToken cancellationToken)
    {
        return _repository.DeleteById(id, cancellationToken);
    }
}
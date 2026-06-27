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

    public IQueryable<TEntity> Query => _repository.Query;
    public void Add(TEntity entity) => _repository.Add(entity);

    public void Delete(TEntity entity) => _repository.Delete(entity);

    public void DeleteById(TId id) => _repository.DeleteById(id);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => _repository.SaveChangesAsync(cancellationToken);

}
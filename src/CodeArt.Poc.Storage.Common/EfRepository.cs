using System.Numerics;

using CodeArt.Poc.Core;

using Microsoft.EntityFrameworkCore;

namespace CodeArt.Poc.Storage.Common;

internal sealed class EfRepository<TEntity, TContext, TId>(TContext context) : IRepository<TEntity, TId>
    where TId : IEquatable<TId>, IEqualityOperators<TId, TId, bool>
    where TEntity : class, IEntity<TId>, new()
    where TContext : DbContext
{
    public IQueryable<TEntity> Query => context.Set<TEntity>();
    public void Add(TEntity entity)
    {
        context.Add(entity);
    }

    public void Delete(TEntity entity)
    {
        context.Remove(entity);
    }

    public void DeleteById(TId id)
    {
        var entity = new TEntity
        {
            Id = id
        };
        context.Remove(entity);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}
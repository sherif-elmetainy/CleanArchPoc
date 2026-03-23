using System.Numerics;
using CodeArt.Poc.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CodeArt.Poc.Storage.Common;

internal class EfRepository<TEntity, TContext, TId>(TContext context) : IRepository<TEntity, TId>
    where TId : IEquatable<TId>, IEqualityOperators<TId, TId, bool>
    where TEntity : class, IEntity<TId>, new()
    where TContext : DbContext
{
    public IQueryable<TEntity> GetQueryable()
    {
        return context.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        context.Set<TEntity>().Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        context.Set<TEntity>().Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public ValueTask<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        return context.FindAsync<TEntity>([id], cancellationToken);
    }

    public async Task<bool> DeleteById(TId id, CancellationToken cancellationToken)
    {
        var entity = new TEntity
        {
            Id = id
        };
        context.Set<TEntity>().Remove(entity);
        var result = await context.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}
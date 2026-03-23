using System.Numerics;

using CodeArt.Poc.Core;

namespace CodeArt.Poc.Storage.Common;

public interface IRepositoryFactory<TEntity, in TId>
    where TId : IEquatable<TId>, IEqualityOperators<TId, TId, bool>
    where TEntity : class, IEntity<TId>, new()
{
    IRepository<TEntity, TId> CreateRepository(IServiceProvider serviceProvider);
}
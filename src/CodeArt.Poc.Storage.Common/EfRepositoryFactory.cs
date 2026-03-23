using System.Numerics;
using CodeArt.Poc.Infrastructure.Abstractions;

namespace CodeArt.Poc.Storage.Common;

public class EfRepositoryFactory<TEntity, TId>
    where TId : IEquatable<TId>, IEqualityOperators<TId, TId, bool>
    where TEntity : class, IEntity<TId>, new()
{
    private static HashSet<Type> s_mainDbContextTypes = InitializeTypes<PocMainDbContext>();

    private static HashSet<Type> InitializeTypes<PocMainDbContext>(Type type)
    {
        throw new NotImplementedException();
    }
}
using System.Numerics;

using CodeArt.Poc.Core;

using Microsoft.Extensions.DependencyInjection;

namespace CodeArt.Poc.Storage.Common;

internal sealed class EfRepositoryFactory<TEntity, TId>
    : IRepositoryFactory<TEntity, TId>
    where TId : IEquatable<TId>, IEqualityOperators<TId, TId, bool>
    where TEntity : class, IEntity<TId>, new()
{
    private readonly Func<IServiceProvider, IRepository<TEntity, TId>> _repositoryFactory;

    public EfRepositoryFactory()
    {
        if (EfContextTypes.IsMainContextType(typeof(TEntity)))
        {
            _repositoryFactory = (sp) =>
            {
                var context = sp.GetRequiredService<PocMainDbContext>();
                return new EfRepository<TEntity, PocMainDbContext, TId>(context);
            };
        }
        else if (EfContextTypes.IsEntityType(typeof(TEntity)))
        {
            _repositoryFactory = (sp) =>
            {
                var context = sp.GetRequiredService<PocDbContext>();
                return new EfRepository<TEntity, PocDbContext, TId>(context);
            };
        }
        else
        {
            throw new InvalidOperationException($"Entity type {typeof(TEntity)} is not supported");
        }
    }


    public IRepository<TEntity, TId> CreateRepository(IServiceProvider serviceProvider)
    {
        return _repositoryFactory(serviceProvider);
    }
}
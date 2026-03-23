using CodeArt.Poc.Core;
using CodeArt.Poc.Storage.Common;
// ReSharper disable once CheckNamespace
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting;


public static class Extensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddStorageServices()
        {
            services.AddSingleton(typeof(IRepositoryFactory<,>), typeof(EfRepositoryFactory<,>));
            services.AddScoped(typeof(IRepository<,>), typeof(RepositoryProxy<,>));
            return services;
        }
    }
}
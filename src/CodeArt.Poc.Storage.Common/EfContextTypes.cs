using System.Collections.Frozen;

using CodeArt.Poc.Core;

using Microsoft.EntityFrameworkCore;

namespace CodeArt.Poc.Storage.Common;

internal static class EfContextTypes
{
    private static readonly FrozenSet<Type> s_mainContextTypes = GetTypesInContext<PocMainDbContext>();
    private static readonly FrozenSet<Type> s_entityTypes = GetTypesInContext<PocDbContext>();

    public static bool IsMainContextType(Type type)
    {
        return s_mainContextTypes.Contains(type);
    }

    public static bool IsEntityType(Type type)
    {
        return s_entityTypes.Contains(type);
    }

    private static FrozenSet<Type> GetTypesInContext<TContext>()
        where TContext : DbContext
    {
        var result = new HashSet<Type>();
        var entityTypes = typeof(TContext)
            .GetProperties()
            .Where(p => p is { CanRead: true, PropertyType: { IsConstructedGenericType: true, GenericTypeArguments.Length: 1 } } &&
                        p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
            .Select(p => p.PropertyType.GenericTypeArguments[0])
            .Where(t => t.IsEntity())
            ;

        foreach (var type in entityTypes)
        {
            result.Add(type);
        }

        return result.ToFrozenSet();
    }

    extension(Type type)
    {
        private bool IsEntity()
        {
            var interfaces = type.GetInterfaces();
            return interfaces.Any(t => t.IsEntityInterface());
        }

        private bool IsEntityInterface()
        {
            return type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(IEntity<>);
        }
    }
}
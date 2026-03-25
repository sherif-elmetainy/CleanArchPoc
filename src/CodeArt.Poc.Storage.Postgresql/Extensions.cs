
using System.Collections.Concurrent;

using CodeArt.Poc.Aspire.ServiceDefaults;
using CodeArt.Poc.Core.Tenants;
using CodeArt.Poc.Storage.Common;
using CodeArt.Poc.Storage.Postgresql;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting;

public static class Extensions
{
    extension(WebApplicationBuilder builder)
    {

        public void AddPostgres(bool addDbContext = true)
        {
            // builder.Services.AddScoped<PocMainDbContext, PostgresqlPocMainDbContext>();
            
            //builder.AddNpgsqlDbContext<PostgresqlPocMainDbContext>(ServiceConstants.Db.Postgres.MainDbName);
            var prefix = ServiceConstants.Db.Postgres.MainDbName.ToUpperInvariant();
            var hostName = builder.Configuration.GetValue<string>($"{prefix}_HOST");
            var port = builder.Configuration.GetValue<int>($"{prefix}_PORT");
            var username = builder.Configuration.GetValue<string>($"{prefix}_USERNAME");
            var password = builder.Configuration.GetValue<string>($"{prefix}_PASSWORD");
            var database = builder.Configuration.GetValue<string>($"{prefix}_DATABASENAME");

            var mainOptionsBuilder = new DbContextOptionsBuilder<PostgresqlPocMainDbContext>();
            mainOptionsBuilder.UseNpgsql($"Host={hostName};Port={port};Database={database};Username={username};Password={password}");

#pragma warning disable EF1001
            var mainPool = new DbContextPool<PostgresqlPocMainDbContext>(mainOptionsBuilder.Options);
            builder.Services.AddSingleton<IDbContextPool<PostgresqlPocMainDbContext>>(mainPool);
            builder.Services.AddScoped<IScopedDbContextLease<PostgresqlPocMainDbContext>, ScopedDbContextLease<PostgresqlPocMainDbContext>>();

            var mainMigrationLock = new Lock();
            var mainMigrated = false;


            builder.Services.AddScoped<PocMainDbContext>(sp =>
            {
                var lease = sp.GetRequiredService<IScopedDbContextLease<PostgresqlPocMainDbContext>>();
                var ctx = lease.Context;
                lock (mainMigrationLock)
                {
                    if (!mainMigrated)
                    {
                        ctx.Database.Migrate();
                        mainMigrated = true;
                    }
                }

                return ctx;
            });

            if (!addDbContext)
            {
                return;
            }
            var poolBag = new ConcurrentDictionary<TenantName, Lazy<DbContextPool<PostgresqlPocDbContext>>>();
            // builder.Services.AddScoped<PocDbContext, PostgresqlPocDbContext>();
            builder.Services.AddScoped<IDbContextPool<PostgresqlPocDbContext>>(sp =>
            {
                var ctp = sp.GetRequiredService<ICurrentTenantProvider>();
                var pool = poolBag.GetOrAdd(ctp.Name, key =>
                {
                    return new Lazy<DbContextPool<PostgresqlPocDbContext>>(() =>
                    {
                        var dbContextOptionsBuilder = new DbContextOptionsBuilder<PostgresqlPocDbContext>();
                        var dbName = key.Value;
                        dbContextOptionsBuilder.UseNpgsql(
                            $"Host={hostName};Port={port};Database={dbName};Username={username};Password={password}");
                        var pool = new DbContextPool<PostgresqlPocDbContext>(dbContextOptionsBuilder.Options);

                        var tempContext = (PocDbContext)pool.Rent();
                        tempContext.Database.Migrate();
                        tempContext.Dispose();
                        return pool;
                    });
                });

                return pool.Value;
            });

            builder.Services.AddScoped<IScopedDbContextLease<PostgresqlPocDbContext>, ScopedDbContextLease<PostgresqlPocDbContext>>();

            // builder.Services.AddScoped<PocDbContext, PostgresqlPocDbContext>();
            builder.Services.AddScoped<PocDbContext>(sp =>
            {
                var lease = sp.GetRequiredService<IScopedDbContextLease<PostgresqlPocDbContext>>();
                var ctx = lease.Context;
                return ctx;
            });
#pragma warning restore EF1001

        }    
    }
    
    
}
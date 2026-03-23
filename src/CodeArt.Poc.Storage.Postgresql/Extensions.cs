
using CodeArt.Poc.Aspire.ServiceDefaults;
using CodeArt.Poc.Storage.Common;
using CodeArt.Poc.Storage.Postgresql;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
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
            var database = builder.Configuration.GetValue<string>($"{prefix}_DATABASE");

            builder.Services.AddScoped<PocMainDbContext>(_ =>
            {
                var builder = new DbContextOptionsBuilder<PostgresqlPocMainDbContext>();
                builder.UseNpgsql($"Host={hostName};Port={port};Database={database};Username={username};Password={password}");
                var ctx = new PostgresqlPocMainDbContext(builder.Options);
                ctx.Database.EnsureCreated();
                return ctx;
            });

            if (!addDbContext)
            {
                return;
            }
            
            // builder.Services.AddScoped<PocDbContext, PostgresqlPocDbContext>();
            builder.Services.AddScoped<PocDbContext>(sp =>
            {
                var ctp = sp.GetRequiredService<ICurrentTenantProvider>();

                var builder = new DbContextOptionsBuilder<PostgresqlPocDbContext>();
                var dbName = ctp.Name;
                builder.UseNpgsql(
                    $"Host={hostName};Port={port};Database={dbName};Username={username};Password={password}");

                var ctx = new PostgresqlPocDbContext(builder.Options);
                ctx.Database.EnsureCreated();
                return ctx;
            });

        }    
    }
    
    
}

using CodeArt.Poc.Aspire.ServiceDefaults;
using CodeArt.Poc.Infrastructure.Abstractions;
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
        public WebApplicationBuilder AddPostgres()
        {
            builder.Services.AddScoped<PocMainDbContext, PostgresqlPocMainDbContext>();
            builder.Services.AddScoped<PocDbContext, PostgresqlPocDbContext>();
            
            builder.AddNpgsqlDbContext<PostgresqlPocMainDbContext>(ServiceConstants.Db.Postgres.MainDbName);
            
            var prefix = ServiceConstants.Db.Postgres.MainDbName.ToUpperInvariant();
            var hostName = builder.Configuration.GetValue<string>($"{prefix}_HOST");
            var port = builder.Configuration.GetValue<int>($"{prefix}_PORT");
            var username = builder.Configuration.GetValue<int>($"{prefix}_USERNAME");
            var password = builder.Configuration.GetValue<int>($"{prefix}_PASSWORD");

            builder.Services.AddScoped<PostgresqlPocDbContext>(sp =>
            {
                var ctp = sp.GetRequiredService<ICurrentTenantProvider>();
                
                var builder = new DbContextOptionsBuilder<PostgresqlPocDbContext>();
                var database = ctp.Name; 
                builder.UseNpgsql($"Host={hostName};Port={port};Database={database};Username={username};Password={password}");
                
                var ctx = new PostgresqlPocDbContext(builder.Options);
                ctx.Database.EnsureCreated();
                ctx.Database.Migrate();
                return ctx;
            });
            return builder;
        }    
    }
    
    
}
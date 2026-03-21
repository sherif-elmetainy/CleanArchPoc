using CodeArt.Poc.Entities;
using CodeArt.Poc.Primitives;

using Microsoft.EntityFrameworkCore;

namespace CodeArt.Poc.Storage.Common;

public abstract class PocMainDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Tenant> Tenants => Set<Tenant>();
    
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.RegisterAllInMainEfCoreConverters();
        configurationBuilder.RegisterAllMainPrimitives();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tenant>()
            .HasData(new Tenant() { Id = TenantId.SystemId, Name = TenantName.From("CodeArt") });
    }
}
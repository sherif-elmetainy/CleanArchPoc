using CodeArt.Poc.Core;
using CodeArt.Poc.Core.Tenants;

using Microsoft.EntityFrameworkCore;

using TenantId = CodeArt.Poc.Core.Tenants.TenantId;
using TenantName = CodeArt.Poc.Core.Tenants.TenantName;

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
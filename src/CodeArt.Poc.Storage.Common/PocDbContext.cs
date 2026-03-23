using CodeArt.Poc.Core;
using CodeArt.Poc.Core.Customers;

using Microsoft.EntityFrameworkCore;

namespace CodeArt.Poc.Storage.Common;

public abstract class PocDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Customer> Customers => Set<Customer>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.RegisterAllInEfCoreConverters();
        configurationBuilder.RegisterAllPrimitives();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CodeArt.Poc.Storage.Postgresql;

public class DesignTimeMainDbContextFactory : IDesignTimeDbContextFactory<PostgresqlPocMainDbContext>
{
    public PostgresqlPocMainDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PostgresqlPocMainDbContext>();
        // Use a design-time connection string for migrations.
        optionsBuilder.UseNpgsql(
            "Host=nowhere;Database=fake_db;Username=fake_user;Password=fake_password;");
        return new PostgresqlPocMainDbContext(optionsBuilder.Options);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CodeArt.Poc.Storage.Postgresql;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PostgresqlPocDbContext>
{
    public PostgresqlPocDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PostgresqlPocDbContext>();
        // Use a design-time connection string for migrations.
        optionsBuilder.UseNpgsql(
            "Host=nowhere;Database=fake_db;Username=fake_user;Password=fake_password;");
        return new PostgresqlPocDbContext(optionsBuilder.Options);
    }
}
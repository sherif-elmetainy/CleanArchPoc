using CodeArt.Poc.Storage.Common;

using Microsoft.EntityFrameworkCore;

namespace CodeArt.Poc.Storage.Postgresql;

public class PostgresqlPocDbContext(DbContextOptions<PostgresqlPocDbContext> options) : PocDbContext(options);
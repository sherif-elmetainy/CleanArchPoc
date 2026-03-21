using CodeArt.Poc.Aspire.ServiceDefaults;
using CodeArt.Poc.Storage.Common;

using Microsoft.EntityFrameworkCore;

namespace CodeArt.Poc.Storage.Postgresql;

public class PostgresqlPocMainDbContext(DbContextOptions<PostgresqlPocMainDbContext> options) : PocMainDbContext(options);

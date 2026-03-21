
using CodeArt.Poc.Aspire.ServiceDefaults;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres(ServiceConstants.Db.Postgres.ServiceName)
    .WithPgAdmin();
var postgresMainDb = postgres.AddDatabase(ServiceConstants.Db.Postgres.MainDbName);

builder.AddProject<Projects.CodeArt_Poc_WebApi>(ServiceConstants.Api.Name)
    .WaitFor(postgresMainDb)
    .WithReference(postgresMainDb);

builder.Build().Run();
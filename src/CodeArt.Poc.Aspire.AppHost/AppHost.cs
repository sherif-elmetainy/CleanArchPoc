
using CodeArt.Poc.Aspire.ServiceDefaults;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres(ServiceConstants.Db.Postgres.ServiceName)
    .WithDataVolume(ServiceConstants.Db.Postgres.VolumeName)
    .WithPgAdmin((c) =>
    {
        c.WithVolume(ServiceConstants.Db.Postgres.PgAdminVolumeName, "/var/lib/pgadmin");
    });
var postgresMainDb = postgres.AddDatabase(ServiceConstants.Db.Postgres.MainDbName);

builder.AddProject<Projects.CodeArt_Poc_WebApi>(ServiceConstants.Api.Name)
    .WaitFor(postgresMainDb)
    .WithReference(postgresMainDb);

builder.AddProject<Projects.CodeArt_Poc_TenantsApi>(ServiceConstants.Api.TenantsName)
    .WaitFor(postgresMainDb)
    .WithReference(postgresMainDb);

builder.Build().Run();
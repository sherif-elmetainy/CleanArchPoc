
using CodeArt.Poc.Aspire.ServiceDefaults;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres(ServiceConstants.Db.Postgres.ServiceName)
    .WithDataVolume(ServiceConstants.Db.Postgres.VolumeName)
    .WithPgAdmin((c) =>
    {
        c.WithVolume(ServiceConstants.Db.Postgres.PgAdminVolumeName, "/var/lib/pgadmin");
    });
var postgresMainDb = postgres.AddDatabase(ServiceConstants.Db.Postgres.MainDbName);

var api = builder.AddProject<Projects.CodeArt_Poc_WebApi>(ServiceConstants.Api.Name)
    .WaitFor(postgresMainDb)
    .WithReference(postgresMainDb);

var tenantsApi = builder.AddProject<Projects.CodeArt_Poc_TenantsApi>(ServiceConstants.Api.TenantsName)
    .WaitFor(postgresMainDb)
    .WithReference(postgresMainDb);

builder.AddJavaScriptApp("tenants-frontend", "../frontend")
    .WithPnpm()
    .WithBuildScript("build:booking")
    .WithRunScript("start:tenants")
    .WithReference(tenantsApi)
    .WithHttpsEndpoint(port: 15172, targetPort: 15176);

builder.AddJavaScriptApp("booking-frontend", "../frontend")
    .WithPnpm()
    .WithBuildScript("build:booking")
    .WithRunScript("start:booking")
    .WithReference(api)
    .WithHttpsEndpoint(port: 15172, targetPort: 15176);


builder.Build().Run();
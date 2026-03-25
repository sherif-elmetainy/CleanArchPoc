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

var tenantsFront = builder.AddJavaScriptApp("tenants-frontend", "../frontend")
    .WithPnpm()
    .WithBuildScript("build:booking")
    .WithRunScript("start:tenants")
    .WithReference(tenantsApi)
    .WithHttpsEndpoint(name: "https", targetPort: 15176);

var bookingFront = builder.AddJavaScriptApp("booking-frontend", "../frontend")
    .WithPnpm()
    .WithBuildScript("build:booking")
    .WithRunScript("start:booking")
    .WithReference(api)
    .WithHttpsEndpoint(name: "https", targetPort: 15175);

tenantsApi.WithEnvironment("CORS__ALLOWEDORIGINS__0", tenantsFront.GetEndpoint("https").Property(EndpointProperty.Url));
api.WithEnvironment("CORS__ALLOWEDORIGINS__0", bookingFront.GetEndpoint("https").Property(EndpointProperty.Url));
builder.Build().Run();
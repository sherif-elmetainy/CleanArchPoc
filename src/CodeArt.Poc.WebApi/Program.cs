using CodeArt.Poc.Core.Customers.Register;
using CodeArt.Poc.Storage.Common;
using CodeArt.Poc.WebApi;
using CodeArt.Poc.WebApi.Customers;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.AddPostgres();
builder.Services.AddStorageServices();
builder.Services.AddSingleton<ICurrentTenantProvider, CurrentTenantProvider>();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        var origins = builder.Configuration
            .GetSection("Cors:AllowedOrigins")
            .Get<string[]>() ?? [];
        policyBuilder.WithOrigins(origins).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    });
});

builder.Services.AddMediator(options =>
{
    // Lifetime: Singleton is fastest per docs; Scoped/Transient also supported.
    options.ServiceLifetime = ServiceLifetime.Scoped;

    // Supply any TYPE from each assembly you want scanned (the generator finds the assembly from the type)
    options.Assemblies =
    [
        typeof(RegisterCommandHandler),                       // Core
    ];

    // // Register pipeline behaviors here (order matters)
    // options.PipelineBehaviors =
    // [
    //     typeof(LoggingBehavior<,>)
    // ];

    // If you have stream behaviors:
    // options.StreamPipelineBehaviors = [ typeof(YourStreamBehavior<,>) ];
});

// Configure the JSON options to use the generated context
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapStaticAssets();

app.UseCors();

app.MapDefaultEndpoints();

app.MapGet("/", () => "Hello World!");
app.MapCustomerEndpoints();
app.Run();
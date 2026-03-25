using CodeArt.Poc.Storage.Common;
using CodeArt.Poc.WebApi;
using CodeArt.Poc.WebApi.Customers;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.AddPostgres();
builder.Services.AddStorageServices();
builder.Services.AddSingleton<ICurrentTenantProvider, CurrentTenantProvider>();

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

app.MapDefaultEndpoints();

app.MapGet("/", () => "Hello World!");
app.MapCustomerEndpoints();
app.Run();
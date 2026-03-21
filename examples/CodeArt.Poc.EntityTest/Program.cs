using CodeArt.Poc.EntityTest;
using CodeArt.Poc.Infrastructure.Abstractions;
using CodeArt.Poc.Storage.Common;
using CodeArt.Poc.Storage.Postgresql;

using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddScoped<PocDbContext, PostgresqlPocDbContext>();
services.AddScoped<ICustomerRepository, CustomerRepository>();
services.AddScoped<App>();

using var serviceProvider = services.BuildServiceProvider();

using var scope = serviceProvider.CreateScope();
var app = scope.ServiceProvider.GetRequiredService<App>();
await app.RunAsync();
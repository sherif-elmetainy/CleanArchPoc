using CodeArt.Poc.Aspire.ServiceDefaults;
using CodeArt.Poc.Core;
using CodeArt.Poc.Core.Customers;
using CodeArt.Poc.Core.Customers.Register;

using Mediator;

using Microsoft.EntityFrameworkCore;

namespace CodeArt.Poc.WebApi.Customers;

public static class Endpoints
{
    extension(IEndpointRouteBuilder app)
    {
        public IEndpointRouteBuilder MapCustomerEndpoints()
        {
            var group = app.MapGroup($"{ServiceConstants.Api.BasePath}/{ServiceConstants.Api.Version}/customers")
                .WithTags("Customers");

            group.MapGet("/", async (IRepository<Customer, CustomerId> repo, CancellationToken cancellationToken) =>
            {
                var tenants = await repo.Query
                    .Select(t => new CustomerRow(t.Id, t.FirstName, t.LastName)).ToListAsync(cancellationToken);
                return Results.Ok(tenants);
            });

            group.MapPost("/", async (IMediator mediator, RegisterRequest request, IRepository<Customer, CustomerId> repo, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new RegisterCommand(request.Email, request.FirstName, request.LastName), cancellationToken);
                return Results.Ok(result);
            });

            return group;
        }
    }
}
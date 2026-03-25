using CodeArt.Poc.Aspire.ServiceDefaults;
using CodeArt.Poc.Core;
using CodeArt.Poc.Core.Customers;

using Microsoft.EntityFrameworkCore;

namespace CodeArt.Poc.WebApi.Customers;

public static class Endpoints
{
    extension(IEndpointRouteBuilder app)
    {
        public IEndpointRouteBuilder MapCustomerEndpoints()
        {
            var group = app.MapGroup($"{ServiceConstants.Api.BasePath}/{ServiceConstants.Api.Version}/customers")
                .WithTags("Tenants");

            group.MapGet("/", async (IRepository<Customer, CustomerId> repo, CancellationToken cancellationToken) =>
            {
                var tenants = await repo.GetQueryable()
                    .Select(t => new CustomerRow(t.Id, t.FirstName, t.LastName)).ToListAsync(cancellationToken);
                return Results.Ok(tenants);
            });

            return group;
        }
    }
}
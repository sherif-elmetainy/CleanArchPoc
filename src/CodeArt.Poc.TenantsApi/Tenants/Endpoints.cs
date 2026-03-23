using CodeArt.Poc.Aspire.ServiceDefaults;
using CodeArt.Poc.Infrastructure.Abstractions;

using Microsoft.EntityFrameworkCore;

namespace CodeArt.Poc.TenantsApi.Tenants;

public static class Endpoints
{
    extension(IEndpointRouteBuilder app)
    {
        public IEndpointRouteBuilder MapTenantEndpoints()
        {
            var group = app.MapGroup($"{ServiceConstants.Api.BasePath}/{ServiceConstants.Api.Version}/tenants")
                .WithTags("Tenants");
            
            group.MapGet("/", async (ITenantsRepository repo, CancellationToken cancellationToken) =>
            {
                var tenants = await repo.GetTenants()
                    .Select(t => new TenantRow()
                    {
                        Id = t.Id,
                        Name = t.Name
                    }).ToListAsync(cancellationToken);
                return Results.Ok(tenants);
            });

            return group;
        }
    }
}
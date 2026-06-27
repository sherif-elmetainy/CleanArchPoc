using CodeArt.Poc.Aspire.ServiceDefaults;
using CodeArt.Poc.Core;
using CodeArt.Poc.Core.Tenants;

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
            
            group.MapGet("/", async (IRepository<Tenant, TenantId> repo, CancellationToken cancellationToken) =>
            {
                var tenants = await repo.Query
                    .Select(t => new TenantRow(t.Id, t.Name)).ToListAsync(cancellationToken);
                return Results.Ok(tenants);
            });

            return group;
        }
    }
}
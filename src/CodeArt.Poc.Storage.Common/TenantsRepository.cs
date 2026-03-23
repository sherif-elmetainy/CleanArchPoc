using CodeArt.Poc.Entities;
using CodeArt.Poc.Infrastructure.Abstractions;

using Microsoft.EntityFrameworkCore;

namespace CodeArt.Poc.Storage.Common;

public class TenantsRepository(PocMainDbContext mainDbContext) : ITenantsRepository
{
    public IQueryable<Tenant> GetTenants()
    {
        return mainDbContext.Tenants.AsNoTracking();
    }
}
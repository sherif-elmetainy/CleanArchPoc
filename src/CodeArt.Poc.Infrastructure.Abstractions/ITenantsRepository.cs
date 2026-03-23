using CodeArt.Poc.Entities;

namespace CodeArt.Poc.Infrastructure.Abstractions;

public interface ITenantsRepository
{
    public IQueryable<Tenant> GetTenants(); 
}
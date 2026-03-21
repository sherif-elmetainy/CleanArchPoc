using CodeArt.Poc.Entities;
using CodeArt.Poc.Primitives;

namespace CodeArt.Poc.Infrastructure.Abstractions;

public interface ICustomerRepository
{
    ValueTask<Customer?> GetByIdAsync(CustomerId id);
    
    Task AddAsync(Customer customer);
    
    Task UpdateAsync(Customer customer);
}
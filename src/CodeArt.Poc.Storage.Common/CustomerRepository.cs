using CodeArt.Poc.Entities;
using CodeArt.Poc.Infrastructure.Abstractions;
using CodeArt.Poc.Primitives;

using Microsoft.EntityFrameworkCore;

namespace CodeArt.Poc.Storage.Common;

public class CustomerRepository(PocDbContext dbContext) : ICustomerRepository
{
    public ValueTask<Customer?> GetByIdAsync(CustomerId id)
    {
        return dbContext.Customers.FindAsync(id);
    }

    public async Task AddAsync(Customer customer)
    {
        dbContext.Customers.Add(customer);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        var entry = dbContext.Customers.Entry(customer);
        entry.State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }
}
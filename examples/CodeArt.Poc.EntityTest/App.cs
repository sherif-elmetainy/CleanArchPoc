using CodeArt.Poc.Entities;
using CodeArt.Poc.Infrastructure.Abstractions;
using CodeArt.Poc.Primitives;

namespace CodeArt.Poc.EntityTest;

public class App(ICustomerRepository repository)
{

    public async Task RunAsync()
    {
        var customer = new Customer { Name = PersonName.From("John") };
        await repository.AddAsync(customer);
    }
    
}
using Mediator;

using Microsoft.EntityFrameworkCore;

namespace CodeArt.Poc.Core.Customers.Register;

public class RegisterCommandHandler(IRepository<Customer, CustomerId> repository) : ICommandHandler<RegisterCommand, CommandResult<CustomerId>>
{
    public async ValueTask<CommandResult<CustomerId>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {

        var existing = await repository.Query.SingleOrDefaultAsync(c => c.Email == command.Email, cancellationToken: cancellationToken);

        if (existing != null)
        {
            return new CommandResult<CustomerId>(CommandResultStatus.Conflict, CustomerId.Uninitialized, $"Customer with email {command.Email} already exists");
        }

        var customer = new Customer
        {
            Id = CustomerId.Uninitialized,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email
        };

        repository.Add(customer);

        try
        {
            await repository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            return new CommandResult<CustomerId>(CommandResultStatus.Conflict, CustomerId.Uninitialized, ex.Message);
        }

        return new CommandResult<CustomerId>(CommandResultStatus.Created, customer.Id, "Customer created");
    }
}
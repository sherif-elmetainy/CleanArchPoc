using Mediator;

namespace CodeArt.Poc.Core.Customers.Register;

public record RegisterCommand(EmailAddress Email, PersonName FirstName, PersonName LastName) : ICommand<CommandResult<CustomerId>>;
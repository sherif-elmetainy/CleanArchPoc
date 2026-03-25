using CodeArt.Poc.Core;
using CodeArt.Poc.Core.Customers;

namespace CodeArt.Poc.WebApi.Customers;

public record CustomerRow(CustomerId Id, PersonName FirstName, PersonName LastName);
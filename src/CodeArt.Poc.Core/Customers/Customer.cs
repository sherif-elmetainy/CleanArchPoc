namespace CodeArt.Poc.Core.Customers;

public class Customer : IEntity<CustomerId>
{
    public CustomerId Id { get; set; }

    public PersonName FirstName { get; set; }
    public PersonName LastName { get; set; }
}
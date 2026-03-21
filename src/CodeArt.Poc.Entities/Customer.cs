using CodeArt.Poc.Primitives;

namespace CodeArt.Poc.Entities;

public class Customer
{
    public CustomerId Id { get; set; }
    
    public PersonName Name { get; set; }
}
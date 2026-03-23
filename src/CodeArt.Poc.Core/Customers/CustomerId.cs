using System.Numerics;

using Vogen;

namespace CodeArt.Poc.Core.Customers;

[ValueObject<long>]
[Instance("Uninitialized", 0, "The uninitialized customer id (which is zero)")]
public readonly partial struct CustomerId : IEqualityOperators<CustomerId, CustomerId, bool>
{
    private static Validation Validate(long input)
    {
        bool isValid = input > 0;
        return isValid ? Validation.Ok : Validation.Invalid("Customer id must be greater than zero");
    }
}
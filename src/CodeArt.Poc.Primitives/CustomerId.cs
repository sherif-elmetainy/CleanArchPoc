using Vogen;

namespace CodeArt.Poc.Primitives;

[ValueObject<long>]
[Instance("Uninitialized", 0, "The uninitialized customer id (which is zero)")]
public readonly partial struct CustomerId
{
    private static Validation Validate(long input)
    {
        bool isValid = input > 0;
        return isValid ? Validation.Ok : Validation.Invalid(ValidationErrors.ObjectIdMustBePositive);
    }
}
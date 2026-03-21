using System.Text.RegularExpressions;

using CaseConverter;

using JetBrains.Annotations;

using Vogen;

namespace CodeArt.Poc.Primitives;

[ValueObject<string>]
public readonly partial struct TenantName
{
    public const int MaxLength = 30;
    public const string RegularExpression = "^[a-z]+(?:_[a-z]+)*$";
    
    private static string NormalizeInput(string input)
    {
        input = input.Trim().ToSnakeCase();
        return input;
    }
    
    private static Validation Validate(string input)
    {
        switch (input)
        {
            case { Length : 0 }:
                return Validation.Invalid(ValidationErrors.PersonNameCannotBeEmpty);
            case { Length : > MaxLength }:
                return Validation.Invalid(string.Format(ValidationErrors.PersonNameTooLong, MaxLength));
            default:
                {
                    var isValid = Regex.IsMatch(input);
                    return isValid ? Validation.Ok : Validation.Invalid(ValidationErrors.PersonNameInvalid);
                }
        }
    }
    
    [UsedImplicitly]
    public static readonly Regex Regex = GetRegex();

    [GeneratedRegex(RegularExpression, RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex GetRegex();
}
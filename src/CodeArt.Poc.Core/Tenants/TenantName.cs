using System.Text.RegularExpressions;
using CaseConverter;
using JetBrains.Annotations;
using Vogen;

namespace CodeArt.Poc.Core.Tenants;

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
                return Validation.Invalid("Tenant name cannot be empty");
            case { Length : > MaxLength }:
                return Validation.Invalid($"Tenant name cannot be longer than {MaxLength} characters");
            default:
                {
                    var isValid = Regex.IsMatch(input);
                    return isValid
                        ? Validation.Ok
                        : Validation.Invalid(
                            "Tenant name contains invalid characters. Only letters and underscores are allowed. Must not start or end with an underscore or have two consecutive underscores.");
                }
        }
    }

    [UsedImplicitly] public static readonly Regex Regex = GetRegex();

    [GeneratedRegex(RegularExpression, RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex GetRegex();
}
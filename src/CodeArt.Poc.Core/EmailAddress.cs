using System.Text.RegularExpressions;

using JetBrains.Annotations;

using Vogen;

namespace CodeArt.Poc.Core;

[ValueObject<string>]
public readonly partial struct EmailAddress
{
    /// <summary>
    /// Maximum length of email
    /// </summary>
    public const int MaxLength = 60;

    /// <summary>
    /// email regular expression
    /// </summary>
    [UsedImplicitly]
    public const string RegularExpression =
        @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    private static string NormalizeInput(string input)
    {
        return input.Trim().ToLowerInvariant();
    }

    private static Validation Validate(string input)
    {
        switch (input)
        {
            case { Length : 0 }:
                return Validation.Invalid("Email name cannot be empty");
            case { Length : > MaxLength }:
                return Validation.Invalid($"Email cannot be longer than {MaxLength} characters");
            default:
                {
                    var isValid = Regex.IsMatch(input);
                    return isValid ? Validation.Ok : Validation.Invalid("Email is not a valid email address.");
                }
        }
    }

    [UsedImplicitly]
    public static readonly Regex Regex = GetRegex();

    [GeneratedRegex(RegularExpression, RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex GetRegex();
}
using System.Text.RegularExpressions;

using JetBrains.Annotations;

using Vogen;

namespace CodeArt.Poc.Core;

[ValueObject<string>]
public readonly partial struct PersonName
{
    /// <summary>
    /// Maximum length of person name
    /// </summary>
    public const int MaxLength = 60;

    /// <summary>
    /// Letter sub expression (includes letters in all languages)
    /// </summary>
    private const string LetterExpression = @"(?:\s|\p{Ll}|\p{Lu}|\p{Lt}|\p{Lo}|\p{Lm})";

    /// <summary>
    /// Person's name regular expression
    /// </summary>
    [UsedImplicitly]
    public const string RegularExpression =
        $@"^{LetterExpression}+(?:(?:\-|'){LetterExpression}+|{LetterExpression})*$";

    private static string NormalizeInput(string input)
    {
        return input.Trim();
    }

    private static Validation Validate(string input)
    {
        switch (input)
        {
            case { Length : 0 }:
                return Validation.Invalid("Person name cannot be empty");
            case { Length : > MaxLength }:
                return Validation.Invalid($"Person name cannot be longer than {MaxLength} characters");
            default:
                {
                    var isValid = Regex.IsMatch(input);
                    return isValid ? Validation.Ok : Validation.Invalid("Person name contains invalid characters. Only letters, dashes and apostrophes are allowed. Must not start or end with a dash or apostrophe or have two consecutive dashes or apostrophes.");
                }
        }
    }

    [UsedImplicitly]
    public static readonly Regex Regex = GetRegex();

    [GeneratedRegex(RegularExpression, RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex GetRegex();
}
using System.Text.RegularExpressions;

using JetBrains.Annotations;

using Vogen;

namespace CodeArt.Poc.Primitives;

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
    public static readonly Regex Regex = GetPersonNameRegex();

    [GeneratedRegex(RegularExpression, RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex GetPersonNameRegex();
}
using CodeArt.Poc.Core;

using Validly;
using Validly.Extensions.Validators.Strings;

namespace CodeArt.Poc.WebApi.Customers;

[Validatable]
public partial class RegisterRequest(string Email, string FirstName, string LastName)
{

    [EmailAddress]
    public string Email { get; init; } = Email;
    [FirstName]
    public string FirstName { get; init; } = FirstName;

    public string LastName { get; init; } = LastName;


}
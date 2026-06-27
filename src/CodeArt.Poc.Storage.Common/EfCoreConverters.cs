using CodeArt.Poc.Core;
using CodeArt.Poc.Core.Customers;
using Vogen;

namespace CodeArt.Poc.Storage.Common;

[EfCoreConverter<CustomerId>]
[EfCoreConverter<PersonName>]
[EfCoreConverter<EmailAddress>]
internal static partial class EfCoreConverters
{
}
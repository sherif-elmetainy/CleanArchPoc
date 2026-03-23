using CodeArt.Poc.Core;
using CodeArt.Poc.Core.Customers;
using Vogen;

namespace CodeArt.Poc.Storage.Common;

[EfCoreConverter<CustomerId>]
[EfCoreConverter<PersonName>]
internal static partial class EfCoreConverters
{
}
using CodeArt.Poc.Primitives;

using Vogen;

namespace CodeArt.Poc.Storage.Common;

[EfCoreConverter<TenantId>]
[EfCoreConverter<TenantName>]
internal static partial class MainEfCoreConverters
{
}
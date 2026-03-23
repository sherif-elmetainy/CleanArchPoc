using Vogen;

using TenantId = CodeArt.Poc.Core.Tenants.TenantId;
using TenantName = CodeArt.Poc.Core.Tenants.TenantName;

namespace CodeArt.Poc.Storage.Common;

[EfCoreConverter<TenantId>]
[EfCoreConverter<TenantName>]
internal static partial class MainEfCoreConverters
{
}
using System.Text.Json.Serialization;

using CodeArt_Poc_Primitives;

using CodeArt.Poc.TenantsApi.Tenants;

namespace CodeArt.Poc.TenantsApi;

[JsonSourceGenerationOptions(Converters = [typeof(VogenTypesFactory)])]
[JsonSerializable(typeof(List<TenantRow>))]
public partial class AppJsonSerializerContext : JsonSerializerContext
{
    
}
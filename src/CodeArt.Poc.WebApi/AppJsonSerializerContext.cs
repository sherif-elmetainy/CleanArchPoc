using System.Text.Json.Serialization;

using CodeArt_Poc_Core;

using CodeArt.Poc.WebApi.Customers;

namespace CodeArt.Poc.WebApi;

[JsonSourceGenerationOptions(Converters = [typeof(VogenTypesFactory)])]
[JsonSerializable(typeof(List<CustomerRow>))]
public partial class AppJsonSerializerContext : JsonSerializerContext
{
    
}
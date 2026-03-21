using CodeArt.Poc.Infrastructure.Abstractions;

namespace CodeArt.Poc.EntityTest;

public class DbNameProvider : IDbNameProvider
{
    public string DbName => "tenant1";
}
using System.ComponentModel.DataAnnotations;

using CodeArt.Poc.Primitives;

namespace CodeArt.Poc.Entities;

public class Tenant
{
    public TenantId Id { get; set; } = TenantId.New();
    
    public TenantName Name { get; set; }
}
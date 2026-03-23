
using CodeArt.Poc.Core;
using CodeArt.Poc.Core.Tenants;

using Microsoft.EntityFrameworkCore;


namespace CodeArt.Poc.Storage.Common;

public static class EfCoreConventions
{
    extension (ModelConfigurationBuilder configurationBuilder) {
        public void RegisterAllPrimitives()
        {
            configurationBuilder.Properties<PersonName>()
                .HaveMaxLength(PersonName.MaxLength)
                .AreUnicode();
        }
        
        public void RegisterAllMainPrimitives()
        {
            configurationBuilder.Properties<TenantName>()
                .HaveMaxLength(TenantName.MaxLength)
                .AreUnicode(false);
        }
    }
}
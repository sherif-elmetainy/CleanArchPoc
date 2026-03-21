using CodeArt.Poc.Primitives;

using Microsoft.EntityFrameworkCore;

namespace CodeArt.Poc.Storage.Common;

public static class EfCoreConventions
{
    extension (ModelConfigurationBuilder configurationBuilder) {
        public ModelConfigurationBuilder RegisterAllPrimitives()
        {
            configurationBuilder.Properties<PersonName>()
                .HaveMaxLength(PersonName.MaxLength)
                .AreUnicode();
            return configurationBuilder;
        }
        
        public ModelConfigurationBuilder RegisterAllMainPrimitives()
        {
            configurationBuilder.Properties<TenantName>()
                .HaveMaxLength(TenantName.MaxLength)
                .AreUnicode(false);
            
            return configurationBuilder;
        }
    }
}
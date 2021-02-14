using Asuka.Database.Mappers;
using Asuka.Database.Resolvers;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel.Resolvers;
using Dommel;

namespace Asuka.Database
{
    /// <summary>
    /// Set up dapper mappers for POCO properties to columns.
    /// </summary>
    public static class CustomMappers
    {
        public static void Initialize()
        {
            // Configure Dapper.FluentMap.
            FluentMapper.Initialize(config =>
            {
                // Entity maps.
                config.AddMap(new TagMap());
            });

            // Dommel mappers.
            DommelMapper.SetColumnNameResolver(new CustomColumnNameResolver());
            DommelMapper.SetKeyPropertyResolver(new CustomKeyPropertyResolver());
            DommelMapper.SetTableNameResolver(new CustomTableNameResolver());
            DommelMapper.SetPropertyResolver(new DommelPropertyResolver());
        }
    }
}

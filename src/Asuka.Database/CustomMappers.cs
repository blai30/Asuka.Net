using Asuka.Database.Mappers;
using Asuka.Database.Models;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
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
            // Configure mapping to snake_case tables and columns.
            // Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            // Configure Dapper.FluentMap conventions.
            FluentMapper.Initialize(config =>
            {
                // Entity maps.
                config.AddMap(new TagMap());
                // config.AddConvention<PropertyTransformConvention>()
                //     .ForEntitiesInCurrentAssembly("Asuka.Database.Models");
                // config.AddConvention<PropertyTransformConvention>()
                //     .ForEntity<Tag>();

                // Dommel mappers.
                config.ForDommel();
            });
            DommelMapper.SetTableNameResolver(new TableNameSnakeCaseResolver());
            DommelMapper.SetColumnNameResolver(new ColumnNameSnakeCaseResolver());
        }
    }
}

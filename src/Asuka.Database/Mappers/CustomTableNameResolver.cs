using System;
using Dommel;
using Humanizer;

namespace Asuka.Database.Mappers
{
    /// <summary>
    /// Maps the POCO PascalCase singular entity names to plural snake_case table names.
    /// </summary>
    public class CustomTableNameResolver : ITableNameResolver
    {
        public string ResolveTableName(Type type)
        {
            string name = type.Name.Pluralize().Underscore().ToLower();
            return name;
        }
    }
}

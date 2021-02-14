using System;
using Dommel;
using Humanizer;

namespace Asuka.Database.Mappers
{
    public class TableNameSnakeCaseResolver : ITableNameResolver
    {
        public string ResolveTableName(Type type)
        {
            Console.WriteLine(type.Name.Pluralize().Underscore().ToLower());
            return type.Name.Pluralize().Underscore().ToLower();
        }
    }
}

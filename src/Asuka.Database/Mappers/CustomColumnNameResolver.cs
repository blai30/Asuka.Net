using System.Reflection;
using Dommel;
using Humanizer;

namespace Asuka.Database.Mappers
{
    /// <summary>
    /// Maps the POCO PascalCase property names to snake_case column names.
    /// </summary>
    public class CustomColumnNameResolver : IColumnNameResolver
    {
        public string ResolveColumnName(PropertyInfo propertyInfo)
        {
            string name = propertyInfo.Name.Underscore().ToLower();
            return name;
        }
    }
}

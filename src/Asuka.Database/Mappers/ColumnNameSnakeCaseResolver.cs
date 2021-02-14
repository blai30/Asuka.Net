using System.Reflection;
using Dommel;
using Humanizer;

namespace Asuka.Database.Mappers
{
    public class ColumnNameSnakeCaseResolver : IColumnNameResolver
    {
        public string ResolveColumnName(PropertyInfo propertyInfo)
        {
            return propertyInfo.Name.Underscore().ToLower();
        }
    }
}

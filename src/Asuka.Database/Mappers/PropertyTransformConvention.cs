using Dapper.FluentMap.Conventions;
using Humanizer;

namespace Asuka.Database.Mappers
{
    public class PropertyTransformConvention : Convention
    {
        public PropertyTransformConvention()
        {
            Properties()
                .Configure(c => c.Transform(s => s.Underscore().ToLower()));
        }
    }
}

using Asuka.Database.Models;
using Dapper.FluentMap.Dommel.Mapping;

namespace Asuka.Database.Mappers
{
    /// <summary>
    /// Dommel entity map for Tag. Must derive from DommelEntityMap, not EntityMap.
    /// </summary>
    public class TagMap : DommelEntityMap<Tag>
    {
        public TagMap()
        {
            Map(p => p.CreatedAt).Ignore();
            Map(p => p.UpdatedAt).Ignore();
        }
    }
}

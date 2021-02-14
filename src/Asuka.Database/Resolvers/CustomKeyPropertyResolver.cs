using System;
using System.Collections.Generic;
using System.Linq;
using Dommel;

namespace Asuka.Database.Resolvers
{
    /// <summary>
    /// Defines the primary key POCO property to be Id or EntityId.
    /// </summary>
    public class CustomKeyPropertyResolver : IKeyPropertyResolver
    {
        public ColumnPropertyInfo[] ResolveKeyProperties(Type type)
        {
            // Property is Id or EntityId.
            var properties = type.GetProperties()
                .Where(p => p.Name == "Id" || p.Name == $"{type.Name}Id");

            List<ColumnPropertyInfo> list = new();
            foreach (var property in properties)
            {
                ColumnPropertyInfo info = new(property);
                list.Add(info);
            }

            return list.ToArray();
        }
    }
}

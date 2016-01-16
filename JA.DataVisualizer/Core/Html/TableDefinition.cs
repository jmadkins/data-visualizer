using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JA.DataVisualizer.Core.Html
{
    internal class TableDefinition<T>
    {
        public TableDefinition()
        {
            Name = "<<Not Set>>";
            Properties = new List<PropertyInfo>();
        }

        public IEnumerable<T> RecordsToDisplay { get; set; }
        public IDictionary<string, object> Attributes { get; set; }
        public string Name { get; set; }
        public List<PropertyInfo> Properties { get; set; }
    }
}

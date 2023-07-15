using System.Reflection;

namespace Zanella.CSV
{
    internal class MappedProperty
    {
        public PropertyInfo Property { get; set; }

        public int Position { get; set; }

        public bool IsRequired { get; set; }

        public string Example { get; set; }
    }
}

using System.Reflection;

namespace Zanella.DocumentHelper.Util
{
    internal class MappedProperty
    {
        internal PropertyInfo Property { get; set; }

        internal int Position { get; set; }

        internal bool IsRequired { get; set; }

        internal string Example { get; set; }
    }
}

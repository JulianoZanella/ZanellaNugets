using System;

namespace Zanella.CSV.Exceptions
{
    /// <summary>
    /// Not found required value
    /// </summary>
    public class RequiredException : Exception
    {
        public int Position { get; private set; }

        public string Example { get; private set; }

        public RequiredException(int position, string example)
            : base($"Required column '{position}'. Example: '{example}'")
        {
            Position = position;
            Example = example;
        }
    }
}

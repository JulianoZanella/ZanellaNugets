using System;

namespace Zanella.DocumentHelper.Exceptions
{
    /// <summary>
    /// Not found required value
    /// </summary>
    public class RequiredException : Exception
    {
        /// <summary>
        /// Column index
        /// </summary>
        public int Position { get; private set; }

        /// <summary>
        /// Example on error (default value)
        /// </summary>
        public string Example { get; private set; }

        /// <summary>
        /// Not found required value
        /// </summary>
        /// <param name="position">Column index</param>
        /// <param name="example">Example on error (default value)</param>
        public RequiredException(int position, string example)
            : base($"Required column '{position}'. Example: '{example}'")
        {
            Position = position;
            Example = example;
        }
    }
}

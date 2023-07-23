using System;

namespace Zanella.DocumentHelper.Exceptions
{
    /// <summary>
    /// Invalid value in position
    /// </summary>
    public class InvalidValueException : Exception
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
        /// Original value in column
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Invalid value in position
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position">Column index</param>
        /// <param name="example">Example on error (default value)</param>
        /// <param name="exception">Original Exception</param>
        public InvalidValueException(string value, int position, string example, Exception exception)
            : base($"Value: '{value}' is invalid in column '{position}'. Example: '{example}'", exception)
        {
            Value = value;
            Position = position;
            Example = example;
        }
    }
}

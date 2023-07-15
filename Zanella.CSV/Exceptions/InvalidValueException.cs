using System;

namespace Zanella.CSV.Exceptions
{
    /// <summary>
    /// Invalid value in position
    /// </summary>
    public class InvalidValueException : Exception
    {
        public int Position { get; private set; }

        public string Example { get; private set; }

        public string Value { get; private set; }

        public InvalidValueException(string value, int position, string example, Exception exception)
            : base($"Value: '{value}' is invalid in column '{position}'. Example: '{example}'", exception)
        {
            Value = value;
            Position = position;
            Example = example;
        }
    }
}

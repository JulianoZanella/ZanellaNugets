using System;

namespace Zanella.CSV.Exceptions
{
    /// <summary>
    /// Line is empty
    /// </summary>
    public class EmptyLineException : Exception
    {
        public EmptyLineException() : base("Empty line")
        {
        }
    }
}

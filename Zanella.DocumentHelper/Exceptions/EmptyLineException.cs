using System;

namespace Zanella.DocumentHelper.Exceptions
{
    /// <summary>
    /// Line is empty
    /// </summary>
    public class EmptyLineException : Exception
    {
        /// <summary>
        /// Line is empty
        /// </summary>
        public EmptyLineException() : base("Empty line")
        {
        }
    }
}

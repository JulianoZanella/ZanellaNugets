﻿using System;

namespace Zanella.DocumentHelper.CSV
{

    /// <summary>
    /// Column Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CSV_ColumnAttribute : Attribute
    {
        /// <summary>
        /// Position in line, column index
        /// </summary>
        public int Position { get; private set; }

        /// <summary>
        /// Indicate if this column is required
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Example for error messages
        /// </summary>
        public string Example { get; set; }

        /// <summary>
        /// Column Header
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Column Attribute
        /// </summary>
        /// <param name="position">Position in line: Start with 0, don't repeat</param>
        public CSV_ColumnAttribute(int position)
        {
            Position = position;
        }
    }
}

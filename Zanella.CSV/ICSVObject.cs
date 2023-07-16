﻿using System;
using System.Collections.Generic;

namespace Zanella.CSV
{
    public interface ICSVObject
    {
        /// <summary>
        /// Number of the line in content
        /// </summary>
        int Line { get; set; }

        /// <summary>
        /// Exceptions on read content:
        /// 
        /// <see cref="Exceptions.EmptyLineException"/>, 
        /// <see cref="Exceptions.InvalidValueException"/>, 
        /// <see cref="Exceptions.RequiredException"/>
        /// </summary>
        IList<Exception> Exceptions { get; set; }
    }
}

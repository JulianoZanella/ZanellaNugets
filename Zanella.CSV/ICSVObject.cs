using System;
using System.Collections.Generic;

namespace Zanella.CSV
{
    public interface ICSVObject
    {
        int Line { get; set; }

        IList<Exception> Exceptions { get; set; }
    }
}

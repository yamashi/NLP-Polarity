using System;
using System.Collections.Generic;

namespace Common
{
    public interface IShortMessage
    {
        String RawContent { get; set; }
        IDictionary<String, Object> Metadata { get; set; } /* Polarité dans les méta données ? */
    }
}

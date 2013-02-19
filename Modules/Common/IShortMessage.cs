using System;
using System.Collections.Generic;

namespace NaturalLanguageProcessing.Polarity.Common
{
    public interface IShortMessage
    {
        String RawContent { get; set; }
        IDictionary<String, Object> Metadata { get; set; }
    }
}

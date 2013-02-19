using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Common
{
    public interface IAnnotatedShortMessage : IShortMessage
    {
        Polarity Polarity { get; set; }
    }
}

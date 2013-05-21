using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Common
{
    public class AnnotatedShortMessage : ShortMessage, IAnnotatedShortMessage
    {
        public AnnotatedShortMessage(string rawContent, Polarity polarity) : base(rawContent)
        {
            Polarity = polarity;
        }

        public Polarity Polarity { get; set; }
    }
}

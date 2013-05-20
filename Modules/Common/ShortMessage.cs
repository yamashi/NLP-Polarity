using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Common
{
    public class ShortMessage : IShortMessage
    {
        private IDictionary<string, object> _metadata;

        public ShortMessage(string rawContent)
        {
            RawContent = rawContent;
            Metadata = new Dictionary<string, object>();
        }

        public string RawContent { get; set; }

        public IDictionary<string, object> Metadata
        {
            get
            {
                return _metadata;
            }

            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException();
                }

                _metadata = value;
            }
        }
    }
}

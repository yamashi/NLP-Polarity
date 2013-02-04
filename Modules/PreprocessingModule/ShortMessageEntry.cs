using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessingModule
{
    class ShortMessageEntry
    {
        enum Polarity{
            POS, SUB, NEU
        };

        private Polarity polarity;
        private uint id;
        private Author author;
        private Dictionary<string, string> metadata;
        private string message;

        private Polarity EntryPolarity
        {
            get { return polarity; }
            set { polarity = value; }
        }


        public uint Id
        {
            get { return id; }
            set { id = value; }
        }


        internal Author Author
        {
            get { return author; }
            set { author = value; }
        }


        public Dictionary<string, string> Metadata
        {
            get { return metadata; }
            set { metadata = value; }
        }
        

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

    }
}

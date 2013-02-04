using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessingModule
{
    class Author
    {
        private string name;
        private string metadata;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Metadata
        {
            get { return metadata; }
            set { metadata = value; }
        }
    }
}

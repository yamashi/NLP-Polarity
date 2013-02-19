using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Preprocessing
{
    public class Parser
    {
        private string message;
        public Parser(string message)
        {
            this.message = message;
        }

        public string[] Parse()
        {
            LinkedList<string> elements = new LinkedList<string>();

            string[] strs = message.Split(new char[] { '\t' });

            elements.AddLast(strs[0]);
            var idx = strs[1].IndexOf('_');
            elements.AddLast(strs[1].Substring(0, idx));
            strs[1] = strs[1].Substring(idx + 1);

            while((idx = strs[1].IndexOf('/')) != -1)
            {
                elements.AddLast(strs[1].Substring(0, idx));
                strs[1] = strs[1].Substring(idx + 1);
            }
            elements.AddLast(strs[1]);
            elements.AddLast(strs[2]);

            return elements.ToArray();
        }
    }
}

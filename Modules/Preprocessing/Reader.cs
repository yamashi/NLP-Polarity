using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Preprocessing
{
    public class Reader
    {
        string path;

        public Reader(string file)
        {
            path = file;
        }

        public string[] Tokenize()
        {
            LinkedList<string> tweets = new LinkedList<string>();
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {
                    tweets.AddLast(sr.ReadLine());
                }
            }
            return tweets.ToArray();
        }
    }
}

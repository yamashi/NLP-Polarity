using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic
{
    public class WordMatrix
    {
        private Dictionary<string, SpeechEntity> _matrix = new Dictionary<string, SpeechEntity>();

        public WordMatrix()
        {
        }

        public void Add(string word, SpeechClass[] speechClasses)
        {
            if (!_matrix.ContainsKey(word))
            {
                SpeechEntity e = new SpeechEntity(word);
                if(speechClasses != null)
                    foreach (var c in speechClasses)
                    {
                        e.Add(c);
                    }
                _matrix.Add(word, e);
            }
        }

        public SpeechEntity this[string word]
        {
            get
            {
                Add(word, null);
                return _matrix[word];
            }
        }

        public Dictionary<string, SpeechEntity> Matrix
        {
            get { return _matrix; }
        }

        public override string ToString() 
        {
            string ret = "Word count : " + Matrix.Count + "\n";
            foreach (var e in Matrix)
            {
                ret += "\"" + e.Key + "\"" + " = " + e.Value + "\n";
            }
            return ret;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic
{
    public class WordMatrix
    {
        private Dictionary<string, Word> _matrix = new Dictionary<string, Word>();
        private Dictionary<string, int> _featureMap = new Dictionary<string, int>();

        public void AddFeature(string name)
        {
            _featureMap.Add(name, _featureMap.Count);
        }

        public void Build()
        {
            _matrix.Clear();
        }

        public void Add(string word)
        {
            if(!_matrix.ContainsKey(word))
                _matrix.Add(word, new Word(this));
        }

        public Word this[string word]
        {
            get
            {
                Add(word);
                return _matrix[word];
            }
        }

        public Dictionary<string, Word> Matrix
        {
            get { return _matrix; }
        }

        public Dictionary<string, int> Features
        {
            get { return _featureMap; }
        }

        public override string ToString() 
        {
            string ret = "WordMatrix\n";
            ret += "Features : " + Features.Count + "\n";

            int i = 0;
            foreach(var e in Features)
            {
                ret += "Feature #" + i + " = " + e.Key + "\n";
                ++i;
            }

            ret += "\nWord count : " + Matrix.Count + "\n";
            foreach (var e in Matrix)
            {
                ret += "\"" + e.Key + "\"" + " = " + e.Value + "\n";
                ++i;
            }
            return ret;
        }
    }
}

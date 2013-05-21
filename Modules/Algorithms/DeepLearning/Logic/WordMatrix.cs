using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic
{
    public class WordMatrix
    {
        private UInt32 _featureCount;
        private Dictionary<string, SpeechEntity> _matrix = new Dictionary<string, SpeechEntity>();

        public WordMatrix(UInt32 featureCount)
        {
            _featureCount = featureCount;
        }

        public void Add(string word)
        {
        }

        public SpeechEntity this[string word]
        {
            get
            {
                Add(word);
                return _matrix[word];
            }
        }

        public UInt32 FeatureCount
        {
            get
            {
                return _featureCount;
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

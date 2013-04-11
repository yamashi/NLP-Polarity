using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic
{
    public class Word
    {
        private double[] _features;

        public Word(WordMatrix wordMatrix)
        {
            _features = new double[wordMatrix.Features.Count];

            Random random = new Random((int)Stopwatch.GetTimestamp());
            for (int i = 0; i < _features.Length; ++i)
            {
                _features[i] = (random.NextDouble() - 0.5) * 10;
            }
        }

        public double this[int i]
        {
            get
            {
                return _features[i];
            }
        }

        public override string ToString()
        {
            string ret = "{ ";
            bool first = true;
            for (int i = 0; i < _features.Length; ++i)
            {
                if(!first)
                {
                    ret += " , ";
                }
                ret += _features[i];

                first = false;
            }
            ret += " }";
            return ret;
        }
    }
}

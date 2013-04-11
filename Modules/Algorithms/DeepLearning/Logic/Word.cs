using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic
{
    public class SpeechEntity
    {
        private double[] _features;
        private string _entity;

        public SpeechEntity(WordMatrix wordMatrix, string entity)
        {
            _entity = entity;
            _features = new double[wordMatrix.Features.Count];

            Random random = new Random((int)Stopwatch.GetTimestamp());
            for (int i = 0; i < _features.Length; ++i)
            {
                _features[i] = (random.NextDouble() - 0.5) * 10;
            }
        }

        public SpeechEntity(double[] features, string entity)
        {
            _entity = entity;
            _features = features;
        }

        public double this[int i]
        {
            get
            {
                return _features[i];
            }
            set
            {
                _features[i] = value;
            }
        }

        public double[] Features
        {
            get
            {
                return _features;
            }
        }

        public string Entity
        {
            get
            {
                return _entity;
            }
        }

        public override string ToString()
        {
            string ret = _entity + " : { ";
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

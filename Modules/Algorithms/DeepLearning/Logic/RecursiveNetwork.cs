using NeuroBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic
{
    public class RecursiveNetwork
    {
        Network _network;
        ConfigNode _node;
        BasicConfig _config;
        WordMatrix matrix;

        double[] _input = null;
        double[] _desired = null;

        public RecursiveNetwork(WordMatrix matrix)
        {
            this.matrix = matrix;

            _input   = new double[matrix.Features.Count * 2];
            _desired = new double[matrix.Features.Count];

            _node = new ConfigNode();
            _config = new BasicConfig(_node);

            _config.BiasNeuronEnable.Value = true;
            _config.LearningRate.Value = 0.3;
        }

        public void BuildNetwork()
        {
            _network = new Network(_node);
            _network.AddLayer(64);
            _network.AddLayer(16);
            _network.AddLayer(32);
            _network.AddLayer(_desired.Length);

            _network.BindInputLayer(_input);
            _network.BindTraining(_desired);

            _network.AutoLinkFeedforward();
        }

        public void Train(string[] sentence, double polarity)
        {
            for (int i = 0; i < sentence.Length - 1; ++i)
            {
                Proximity(sentence[i], sentence[i + 1]);
            }
        }

        private void Proximity(string a, string b)
        {
            Word wA = matrix[a];
            Word wB = matrix[b];

            ExtractParameters(wA, wB);

            /*wA[0] = (wA[0] + wB[0]) / 2;
            wB[1] = (wA[1] + wB[1]) / 2;

            _network.TrainCurrentPattern(true, false);*/
        }

        private void ExtractParameters(Word a, Word b)
        {
            for (int i = 0; i < _desired.Length; ++i)
            {
                _input[i] = a[i];
                _input[i + _desired.Length] = b[i];
            }
        }
    }
}

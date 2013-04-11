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
            _network.AddLayer(4);
            _network.AddLayer(16);
            _network.AddLayer(_desired.Length);

            _network.BindInputLayer(_input);
            _network.BindTraining(_desired);

            _network.AutoLinkFeedforward();
        }

        public double[] Think(string sentence)
        {
            return Think(sentence.Split(' '));
        }

        public double[] Think(string[] sentence)
        {
            SpeechEntity[] ents = new SpeechEntity[sentence.Length];
            for (int i = 0; i < sentence.Length; ++i)
            {
                ents[i] = matrix[sentence[i]];
            }

            return Recurse(ents);
        }

        private double[] Recurse(SpeechEntity[] weights)
        {
            SpeechEntity best = null;
            double bestRet = Double.NegativeInfinity;
            int startIdx = -1;

            if (weights.Length == 2)
            {
                ExtractParameters(weights[0], weights[1]);
                _network.CalculateFeedforward();

                double[] res = _network.CollectOutput();

                Console.WriteLine("Final : " + new SpeechEntity(res, weights[0].Entity + " " + weights[1].Entity));

                return res;
            }

            for (int i = 0; i < weights.Length - 1; ++i)
            {
                ExtractParameters(weights[i], weights[i + 1]);
                _network.CalculateFeedforward();

                double[] res = _network.CollectOutput();

                double t = res[0] + res[1];

                Console.WriteLine(new SpeechEntity(res, weights[i].Entity + " " + weights[i + 1].Entity));

                if (t > bestRet)
                {
                    startIdx = i;
                    best = new SpeechEntity(res, weights[i].Entity + " " + weights[i + 1].Entity);
                    bestRet = t;
                }
            }

            Console.WriteLine("Best : " + best);

            SpeechEntity[] next = new SpeechEntity[weights.Length - 1];
            for (int i = 0, j = 0; i < next.Length; ++i, ++j)
            {
                if (startIdx == i)
                {
                    next[i] = best;
                    j += 1;
                }
                else
                {
                    next[i] = weights[j];
                }
            }

            return Recurse(next);
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
            SpeechEntity wA = matrix[a];
            SpeechEntity wB = matrix[b];

            ExtractParameters(wA, wB);

            _desired[0] = 10.0f;
            _desired[1] = 10.0f;

            _network.TrainCurrentPattern(true, false);
            _network.CalculateFeedforward();

            double[] res = _network.CollectOutput();

            wA[0] = (res[0] - wA[0]) * 0.1 + wA[0];
            wA[1] = (res[1] - wA[1]) * 0.1 + wA[1];

            wB[0] = (res[0] - wB[0]) * 0.1 + wB[0];
            wB[1] = (res[1] - wB[1]) * 0.1 + wB[1];

        }

        private void ExtractParameters(SpeechEntity a, SpeechEntity b)
        {
            for (int i = 0; i < _desired.Length; ++i)
            {
                _input[i] = a[i];
                _input[i + _desired.Length] = b[i];
            }
        }
    }
}

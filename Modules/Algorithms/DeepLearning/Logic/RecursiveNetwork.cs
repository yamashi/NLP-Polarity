using NeuroBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic
{
    public class RecursiveNetwork
    {
        GriotNet.NeuralNetwork _network;
        WordMatrix matrix;
        double[] _inputs = null;

        public RecursiveNetwork(WordMatrix matrix)
        {
            this.matrix = matrix;

            _inputs = new double[matrix.FeatureCount * 2];
            _network = new GriotNet.NeuralNetwork();
        }

        public void BuildNetwork()
        {
            _network.AddLayer(matrix.FeatureCount * 2);
            _network.AddLayer(64);
            _network.AddLayer(4);
            _network.AddLayer(16);
            _network.AddLayer(matrix.FeatureCount);
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

        private double[] Recurse(SpeechEntity[] entities)
        {
            SpeechEntity best = null;
            double bestRet = Double.NegativeInfinity;
            int startIdx = -1;

            if (entities.Length == 2)
            {
                //ExtractParameters(entities[0], entities[1]);
                double[] res = _network.Evaluate(_inputs);

                //Console.WriteLine("Final : " + new SpeechEntity(res, entities[0].Entity + " " + entities[1].Entity));

                return res;
            }

            for (int i = 0; i < entities.Length - 1; ++i)
            {
                //ExtractParameters(entities[i], entities[i + 1]);

                double[] res = _network.Evaluate(_inputs);

                double t = res[0] + res[1];

                //Console.WriteLine(new SpeechEntity(res, entities[i].Entity + " " + entities[i + 1].Entity));

                if (t > bestRet)
                {
                    startIdx = i;
                  //  best = new SpeechEntity(res, entities[i].Entity + " " + entities[i + 1].Entity);
                    bestRet = t;
                }
            }

            Console.WriteLine("Best : " + best);

            SpeechEntity[] next = new SpeechEntity[entities.Length - 1];
            for (int i = 0, j = 0; i < next.Length; ++i, ++j)
            {
                if (startIdx == i)
                {
                    next[i] = best;
                    j += 1;
                }
                else
                {
                    next[i] = entities[j];
                }
            }

            return Recurse(next);
        }

        public void Train(string[] sentence, double polarity)
        {
        }

    }
}

using NeuroBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic
{
    public class RecursiveNetwork
    {
        private WordMatrix _matrix;
        private GriotNet.NeuralNetwork _network;

        public RecursiveNetwork(WordMatrix matrix)
        {
            this._matrix = matrix;

            _network = new GriotNet.NeuralNetwork();

            _network.AddLayer(20);
            _network.AddLayer(5);
            _network.AddLayer(30);
            _network.AddLayer(8);
            _network.AddLayer(11);
        }

        public void Train(List<double[]> examples, List<double[]> results)
        {
            _network.Learn(examples, results, 0.1);
        }

       public void Parse(string sentence)
        {
            string[] words = sentence.Split(" ".ToCharArray());
            SpeechEntity[] ents = new SpeechEntity[words.Length];
            for (int i = 0; i < words.Length; ++i)
            {
                ents[i] = _matrix[words[i]];
            }
            Parse(ents);
        }

        void Parse(SpeechEntity[] entities)
        {
            double bestScore = -1.0;
            int idx = -1;

            double[] result = null;

            for (int i = 0; i < entities.Length - 1; ++i)
            {
                var resu = _network.Evaluate(SpeechEntity.Group(entities[i], entities[i + 1]));
                if (resu[11] > bestScore)
                {
                    bestScore = resu[11];
                    idx = i;

                    result = (double[])resu.Clone();
                }
            }

            var resEntity = new SpeechEntity(entities[idx], entities[idx + 1], result);

            Console.WriteLine("{0}", resEntity);
            if (entities.Length > 2)
            {
                SpeechEntity[] ents = new SpeechEntity[entities.Length - 1];
                for (int i = 0, j = 0; i < entities.Length; ++i, ++j)
                {
                    if (i == idx)
                    {
                        ents[j] = resEntity;
                        i += 1;
                        continue;
                    }
                    else
                    {
                        ents[j] = entities[i];
                    }
                }

                Parse(ents);
            }
        }
    }
}

using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning;
using NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic;
using System.Collections.Generic;

namespace Tester
{
    class Program
    {
        static GriotNet.NeuralNetwork net = new GriotNet.NeuralNetwork();

        static void Parse(SpeechEntity[] entities)
        {
            double bestScore = -1.0;
            int idx = -1;

            double[] result = null;
            
            for (int i = 0; i < entities.Length - 1; ++i)
            {
                var resu = net.Evaluate(SpeechEntity.Group(entities[i], entities[i + 1]));
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
                for(int i = 0, j = 0; i < entities.Length; ++i, ++j)
                {
                    if(i == idx)
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

        static void Main(string[] args)
        {
            /*Reader reader = new Reader("hollande_train_1.txt");
            var tweets = reader.Tokenize();
            Parser parser = new Parser(tweets[8442]);
            var data = parser.Parse();

            for(var i = 0; i < data.Length; ++i)
            {
                Console.WriteLine(data[i]);
            }
            */

            net.AddLayer(20);
            net.AddLayer(5);
            net.AddLayer(30);
            net.AddLayer(8);
            net.AddLayer(11);

            List<double[]> ex = new List<double[]>();
            List<double[]> res = new List<double[]>();

            SpeechEntity v = new SpeechEntity("");
            SpeechEntity det = new SpeechEntity("_DET_");
            det.Add(SpeechClass._DET_);
            SpeechEntity noun = new SpeechEntity("_NOUN_");
            noun.Add(SpeechClass._NOUN_);
            SpeechEntity verb = new SpeechEntity("_VERB_");
            verb.Add(SpeechClass._VERB_);
            SpeechEntity adj = new SpeechEntity("_ADJ_");
            adj.Add(SpeechClass._ADJ_);
            SpeechEntity adp = new SpeechEntity("_ADP_");
            adj.Add(SpeechClass._ADP_);

            
            ex.Add(SpeechEntity.Group(noun, verb)); res.Add(SpeechEntity.Merge(noun, verb).ToArray().Concat(new double[] { 0.5 }).ToArray());
            ex.Add(SpeechEntity.Group(verb, verb)); res.Add(verb.ToArray().Concat(new double[] { 1.0 }).ToArray());
            ex.Add(SpeechEntity.Group(det, noun)); res.Add(noun.ToArray().Concat(new double[] { 0.9 }).ToArray());
            ex.Add(SpeechEntity.Group(det, adj)); res.Add(det.ToArray().Concat(new double[] { -1.0 }).ToArray());
            ex.Add(SpeechEntity.Group(adj, noun)); res.Add(noun.ToArray().Concat(new double[] { 1.0 }).ToArray());
            ex.Add(SpeechEntity.Group(verb, det)); res.Add(verb.ToArray().Concat(new double[] { -1.0 }).ToArray());
            ex.Add(SpeechEntity.Group(noun, adp)); res.Add(noun.ToArray().Concat(new double[] { 0.5 }).ToArray());
            ex.Add(SpeechEntity.Group(adp, det)); res.Add(adp.ToArray().Concat(new double[] { -1.0 }).ToArray());

            for (int i = 0; i < 1000; ++i)
            {
                net.Learn(ex, res, 0.1);
                double error = net.EvaluateQuadraticError(ex, res);
                //Console.WriteLine("Error {0}", error);
            }

            SpeechEntity the = new SpeechEntity("the");
            the.Add(SpeechClass._DET_);
            SpeechEntity angry = new SpeechEntity("angry");
            angry.Add(SpeechClass._ADJ_);
            SpeechEntity mean = new SpeechEntity("mean");
            mean.Add(SpeechClass._ADJ_);
            SpeechEntity green = new SpeechEntity("green");
            green.Add(SpeechClass._ADJ_);
            SpeechEntity red = new SpeechEntity("red");
            red.Add(SpeechClass._ADJ_);
            SpeechEntity cat = new SpeechEntity("cat");
            cat.Add(SpeechClass._NOUN_);
            SpeechEntity mouse = new SpeechEntity("mouse");
            mouse.Add(SpeechClass._NOUN_);
            SpeechEntity carpet = new SpeechEntity("carpet");
            carpet.Add(SpeechClass._NOUN_);
            SpeechEntity outside = new SpeechEntity("outside");
            outside.Add(SpeechClass._NOUN_);
            SpeechEntity _is = new SpeechEntity("is");
            _is.Add(SpeechClass._VERB_);
            SpeechEntity eating = new SpeechEntity("eating");
            eating.Add(SpeechClass._VERB_);
            SpeechEntity on = new SpeechEntity("on");
            on.Add(SpeechClass._ADP_);

            Parse(new SpeechEntity[] { the, mean, angry, cat, _is, eating, the, green, mouse, on, the, red, carpet });

           /* DeepLearningAlgorithmFactory factory = new DeepLearningAlgorithmFactory();
            var algorithm = factory.NewInstance();

            WordMatrix matrix = new WordMatrix(50, "paths.txt");

            RecursiveNetwork network = new RecursiveNetwork(matrix);
            network.BuildNetwork();

            string[] samples = new string[]
            {
                "I like poneys",
                "I hate poneys",
                "Poneys are cool",
                "Poneys are pretty",
                "I like flowers",
                "You like flowers",
                "I enjoy cool poneys"
            };

            Train(network, samples);

            double[] ret = network.Think("I like cool poneys");

            Console.WriteLine(matrix);

            /*algorithm.Learn(null);
            algorithm.Analyse(null);*/

            Console.ReadKey();
        }

        static void Train(RecursiveNetwork net, string[] samples)
        {
            ArrayList words = new ArrayList();

            for (int i = 0; i < samples.Length; ++i)
            {
                string[] w = samples[i].Split(' ');
                words.Add(w);
            }

            for (int j = 0; j < 50; ++j )
                for (int i = 0; i < words.Count; ++i)
                    net.Train((string[])words[i], 1.0);
        }
    }
}

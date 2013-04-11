using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning;
using NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic;

namespace Tester
{
    class Program
    {
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

            DeepLearningAlgorithmFactory factory = new DeepLearningAlgorithmFactory();
            var algorithm = factory.NewInstance();

            WordMatrix matrix = new WordMatrix();

            matrix.AddFeature("Coherence1D");
            matrix.AddFeature("Coherence2D");
            matrix.AddFeature("Polarity");

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

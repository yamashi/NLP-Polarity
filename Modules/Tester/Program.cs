using System;
using System.Collections.Generic;
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

            matrix.AddFeature("Proximity1D");
            matrix.AddFeature("Proximity2D");
            matrix.AddFeature("Polarity");
            matrix.AddFeature("Coherence");

            RecursiveNetwork network = new RecursiveNetwork(matrix);
            network.BuildNetwork();

            string exampleString = "there is a cat";
            string[] words = exampleString.Split(' ');

            for (int i = 0; i < 100; ++i )
                network.Train(words, 1.0);
            
            Console.WriteLine(matrix);

            algorithm.Learn(null);
            algorithm.Analyse(null);

            Console.ReadKey();
        }
    }
}

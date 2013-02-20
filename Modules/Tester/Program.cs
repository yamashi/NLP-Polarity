using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic;
using NaturalLanguageProcessing.Polarity.Preprocessing;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Reader reader = new Reader("hollande_train_1.txt");
            var tweets = reader.Tokenize();
            Parser parser = new Parser(tweets[8442]);
            var data = parser.Parse();

            for(var i = 0; i < data.Length; ++i)
            {
                Console.WriteLine(data[i]);
            }

            Perceptron per = new Perceptron(2);
            for (int i = 0; i < 100000; ++i)
            {
                per.Learn(new float[] { 1, 0 }, 1);
                per.Learn(new float[] { 0, 1 }, 1);
                per.Learn(new float[] { 1, 1 }, 1);
                per.Learn(new float[] { 0, 0 }, -1);
            }
            Console.WriteLine(per.NormalizeOutput(new float[] { 1, 0 }));
            Console.WriteLine(per.NormalizeOutput(new float[] { 0, 1 }));
            Console.WriteLine(per.NormalizeOutput(new float[] { 1, 1 }));
            Console.WriteLine(per.NormalizeOutput(new float[] { 0, 0 }));

            Console.ReadKey();
        }
    }
}

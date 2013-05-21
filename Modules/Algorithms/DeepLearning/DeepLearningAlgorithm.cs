using System;

using NaturalLanguageProcessing.Polarity.Common;
using NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning
{
    public class DeepLearningAlgorithm : IAlgorithm
    {
       // private WordMatrix _matrix = new WordMatrix();

        public void Learn(IAnnotatedCorpus corpus)
        {
            /*_matrix.AddFeature("Test");
            _matrix.AddFeature("Toto");

            _matrix.Build();

            _matrix.Add("Jesus");

            Console.WriteLine(_matrix.Matrix["Jesus"]);*/
        }

        public IAnnotatedCorpus Analyse(ICorpus corpus)
        {
            throw new NotImplementedException();
        }
    }
}

using System;

using NaturalLanguageProcessing.Polarity.Common;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning
{
    class DeepLearningAlgorithmFactory : IAlgorithmFactory
    {
        public IAlgorithm NewInstance()
        {
            return new DeepLearningAlgorithm();
        }
    }
}

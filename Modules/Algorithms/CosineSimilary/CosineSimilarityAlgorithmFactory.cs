using System;

using NaturalLanguageProcessing.Polarity.Common;

namespace NaturalLanguageProcessing.Polarity.Algorithms.CosineSimilarity
{
    class CosineSimilarityAlgorithmFactory : IAlgorithmFactory
    {
        public IAlgorithm NewInstance()
        {
            return new CosineSimilarityAlgorithm();
        }
    }
}

using System;

namespace NaturalLanguageProcessing.Polarity.Common
{
    public interface IAlgorithmFactory
    {
        IAlgorithm NewInstance();
    }
}

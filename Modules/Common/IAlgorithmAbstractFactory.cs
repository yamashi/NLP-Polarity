using System;

namespace NaturalLanguageProcessing.Polarity.Common
{
    public interface IAlgorithmAbstractFactory
    {
        IAlgorithm Instanciate();
    }
}

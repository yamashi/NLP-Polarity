using System;

namespace NaturalLanguageProcessing.Polarity.Common
{
    public interface IAlgorithm
    {
        void Learn(IAnnotatedCorpus corpus);
        void Analyse(ICorpus corpus);
    }
}

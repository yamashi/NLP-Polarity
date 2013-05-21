using System;

namespace NaturalLanguageProcessing.Polarity.Common
{
    public interface IAlgorithm
    {
        void Learn(IAnnotatedCorpus corpus);
        IAnnotatedCorpus Analyse(ICorpus corpus);
    }
}

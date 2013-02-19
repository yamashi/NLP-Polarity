using System;

namespace NaturalLanguageProcessing.Polarity.Common
{
    public interface IAlgorithm
    {
        void Learn(ICorpus<IAnnotatedShortMessage> corpus);
        void Analyse(ICorpus<IShortMessage> corpus);
    }
}

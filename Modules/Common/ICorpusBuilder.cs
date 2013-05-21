using System;
using System.Collections.Generic;

namespace NaturalLanguageProcessing.Polarity.Common
{
    public interface ICorpusBuilder : ISet<IShortMessage>
    {
        ICorpus ToCorpus();
    }

    public interface IAnnotatedCorpusBuilder : ISet<IAnnotatedShortMessage>
    {
        IAnnotatedCorpus ToCorpus();
    }
}

using System;
using System.Collections.Generic;

namespace NaturalLanguageProcessing.Polarity.Common
{
    public interface ICorpusBuilder : ICollection<IShortMessage>
    {
        ICorpus ToCorpus();
    }

    public interface IAnnotatedCorpusBuilder : ICollection<IAnnotatedShortMessage>
    {
        IAnnotatedCorpus ToCorpus();
    }
}

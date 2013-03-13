using System;
using System.Collections.Generic;

namespace NaturalLanguageProcessing.Polarity.Common
{
    class CorpusBuilder : HashSet<IShortMessage>, ICorpusBuilder
    {
        public ICorpus ToCorpus()
        {
            ICorpus corpus = new Corpus(this);

            return corpus;
        }
    }
}

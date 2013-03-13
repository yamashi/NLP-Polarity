using System;
using System.Collections.Generic;

namespace NaturalLanguageProcessing.Polarity.Common
{
    public class AnnotatedCorpusBuilder : HashSet<IAnnotatedShortMessage>, IAnnotatedCorpusBuilder
    {
        public IAnnotatedCorpus ToCorpus()
        {
            IAnnotatedCorpus corpus = new AnnotatedCorpus(this);

            return corpus;
        }
    }
}

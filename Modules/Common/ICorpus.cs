using System;
using System.Collections.Generic;

namespace NaturalLanguageProcessing.Polarity.Common
{
    public interface ICorpus : IEnumerable<IShortMessage>
    {
    }

    public struct Distribution
    {
    }

    public interface IAnnotatedCorpus : ICorpus, IEnumerable<IAnnotatedShortMessage>
    {
        Distribution GetDistribution();
    }
}

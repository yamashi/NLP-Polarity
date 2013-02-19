using System;
using System.Collections.Generic;

namespace NaturalLanguageProcessing.Polarity.Common
{
    public interface ICorpus<MessageType> : ISet<MessageType> where MessageType : IShortMessage
    {
    }

    public interface ICorpus : ICorpus<IShortMessage>
    {
    }

    public struct Distribution
    {
    }

    public interface IAnnotatedCorpus : ICorpus<IAnnotatedShortMessage>
    {
        Distribution GetDistribution();
    }
}

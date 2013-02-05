using System;
using System.Collections.Generic;

namespace NaturalLanguageProcessing.Polarity.Common
{
    /* Ou ICorpus : ISet<IShortMessage> ??? */
    public interface ICorpus<MessageType> : ISet<MessageType> where MessageType : IShortMessage
    {
    }
}

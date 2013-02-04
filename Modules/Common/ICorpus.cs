using System;
using System.Collections.Generic;

namespace Common
{
    /* Ou ICorpus : ISet<ISHortMessage> ??? */
    public interface ICorpus<MessageType> : ISet<MessageType> where MessageType : IShortMessage
    {
    }
}

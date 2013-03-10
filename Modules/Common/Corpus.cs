using System;
using System.Collections.Generic;

namespace NaturalLanguageProcessing.Polarity.Common
{
    class Corpus : ICorpus
    {
        private ISet<IShortMessage> _impl;

        public Corpus()
        {
            _impl = null;
        }

        public IEnumerator<IShortMessage> GetEnumerator()
        {
            return _impl.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _impl.GetEnumerator();
        }
    }
}

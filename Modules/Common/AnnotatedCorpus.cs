using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Common
{
    class AnnotatedCorpus : IAnnotatedCorpus, IEnumerable<IAnnotatedShortMessage>
    {
        private ISet<IAnnotatedShortMessage> _impl;

        internal AnnotatedCorpus(ISet<IAnnotatedShortMessage> messages)
        {
            _impl = messages;
        }

        public uint Count
        {
            get
            {
                return (uint)_impl.Count;
            }
        }

        public Distribution GetDistribution()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IShortMessage> GetEnumerator()
        {
            return _impl.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _impl.GetEnumerator();
        }

        IEnumerator<IAnnotatedShortMessage> IEnumerable<IAnnotatedShortMessage>.GetEnumerator()
        {
            return _impl.GetEnumerator();
        }
    }
}

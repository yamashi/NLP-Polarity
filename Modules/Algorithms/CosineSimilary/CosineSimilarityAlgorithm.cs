using System;
using System.Collections.Generic;

using NaturalLanguageProcessing.Polarity.Common;

namespace NaturalLanguageProcessing.Polarity.Algorithms.CosineSimilarity
{
    class CosineSimilarityAlgorithm : IAlgorithm
    {
        /* Pour chaque terme, le nombre de fois où il apparait dans un message marqué par une catégorie */
        private IDictionary<string, uint> positive;
        private IDictionary<string, uint> negative;
        private IDictionary<string, uint> neutral;
        
        public CosineSimilarityAlgorithm()
        {
            positive = new Dictionary<string, uint>();
            negative = new Dictionary<string, uint>();
            neutral = new Dictionary<string, uint>();
        }

        public void Learn(IAnnotatedCorpus corpus)
        {
            IEnumerable<IAnnotatedShortMessage> e = corpus;

            foreach (IAnnotatedShortMessage message in e)
            {
                switch (message.Polarity)
                {
                    case Polarity.Common.Polarity.POSITIVE:
                        LearnMessage(message, positive);
                        break;
                    case Polarity.Common.Polarity.NEGATIVE:
                        LearnMessage(message, negative);
                        break;
                    case Polarity.Common.Polarity.NEUTRAL:
                        LearnMessage(message, neutral);
                        break;
                }
            }
        }

        public IAnnotatedCorpus Analyse(ICorpus corpus)
        {
            Analyzer analyzer = new Analyzer(positive, negative, neutral, corpus);

            return analyzer.Result();
        }

        private void LearnMessage(IAnnotatedShortMessage message, IDictionary<string, uint> category)
        {
            char[] separator = new char[1];
            separator[0] = ' ';

            ISet<string> words = new HashSet<string>(message.RawContent.Split(separator));

            foreach(string word in words)
            {
                if (category.ContainsKey(word))
                {
                    category[word] = category[word] + 1;
                }

                else
                {
                    category[word] = 1;
                }
            }
        }
    }
}

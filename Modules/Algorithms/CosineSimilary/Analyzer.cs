using System;
using System.Collections.Generic;

using NaturalLanguageProcessing.Polarity.Common;

namespace NaturalLanguageProcessing.Polarity.Algorithms.CosineSimilarity
{
    class Analyzer
    {
        private ICorpus corpus;
        private IDictionary<string, uint> positive;
        private IDictionary<string, uint> negative;
        private IDictionary<string, uint> neutral;

        public Analyzer(IDictionary<string, uint> pos, IDictionary<string, uint> neg, IDictionary<string, uint> neu, ICorpus cor)
        {
            corpus = cor;
            positive = pos;
            negative = neg;
            neutral = neu;
        }

        public IAnnotatedCorpus Result()
        {
            AnnotatedCorpusBuilder builder = new AnnotatedCorpusBuilder();

            foreach (IShortMessage message in corpus)
            {
                Polarity.Common.Polarity polarity = AnalyseMessage(message);

                builder.Add(new AnnotatedShortMessage(message.RawContent, polarity));
            }

            return builder.ToCorpus();
        }

        private Polarity.Common.Polarity AnalyseMessage(IShortMessage message)
        {
            double simPos = Similarity(message, positive);

            if (simPos > 3.1)
            {
                return Polarity.Common.Polarity.POSITIVE;
            }

            double simNeg = Similarity(message, negative);

            if (simNeg > 3.1)
            {
                return Polarity.Common.Polarity.NEGATIVE;
            }

            Polarity.Common.Polarity polarity = Polarity.Common.Polarity.NEUTRAL;

            if (Math.Abs(simPos - simNeg) < 0.4)
            {
                return polarity;
            }

            else if (simPos > simNeg)
            {
                polarity = Polarity.Common.Polarity.POSITIVE;
            }

            else
            {
                polarity = Polarity.Common.Polarity.NEGATIVE;
            }

            return polarity;
        }

        private double Similarity(IShortMessage message, IDictionary<string, uint> category)
        {
            return 0.0;
        }

        private double Weight(string word, IShortMessage message)
        {
            string[] sep = new string[1];
            sep[0] = word;

            int tf = message.RawContent.Split(sep, StringSplitOptions.RemoveEmptyEntries).Length - 1;

            double df = 0;

            foreach(IShortMessage m in corpus)
            {
                if(m.RawContent.IndexOf(word) >= 0)
                {
                    ++df;
                }
            }

            double idf = Math.Log(((double)corpus.Count) / df);

            double gini = Gini(word);

            return tf * idf * gini;
        }

        private double Gini(string word)
        {
            // TODO
        }
    }
}

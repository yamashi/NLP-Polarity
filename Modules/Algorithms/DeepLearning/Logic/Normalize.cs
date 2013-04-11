using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic
{
    public class Normalize
    {
        public enum Output
        {
            Fire, Neutral, Sleep
        }

        public static Output Do(double input)
        {
            if (input > 0.3) return Output.Fire;
            if (input < -0.3) return Output.Sleep;
            return Output.Neutral;
        }
    }
}

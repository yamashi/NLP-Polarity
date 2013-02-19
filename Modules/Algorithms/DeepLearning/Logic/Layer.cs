using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic
{
    interface Layer
    {
        public float[] ForwardPropagate(float[] inputs);
        public float[] BackPropagate(float[] inputs);
    }
}

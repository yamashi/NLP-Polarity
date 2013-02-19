using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic
{
    interface Layer
    {
        float[] ForwardPropagate(float[] inputs);
        float[] BackPropagate(float[] inputs);
    }
}

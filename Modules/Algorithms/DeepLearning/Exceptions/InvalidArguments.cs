using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Exceptions
{
    class InvalidArguments : Exception
    {
        public InvalidArguments()
        {

        }

        public InvalidArguments(string message)
        : base(message)
        {

        }

        public InvalidArguments(string message, Exception inner)
        : base(message, inner)
        {

        }
    }
}

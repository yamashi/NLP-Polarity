using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GriotNet
{
    public class Neuron
    {
        private double[] mWeights;
        private double mActivation;

        public Neuron(UInt32 pNeuronDendrites)
        {
            mWeights = new double[pNeuronDendrites];

            for (var i = 0; i < pNeuronDendrites; ++i)
            {
                double r = Randomizer.RandNumber() - 0.5f;
                mWeights[i] = r;
                Console.WriteLine(r);
            }
        }

        public double Activate(double[] inputs)
        {
            mActivation = 0.0;

            for(var i = 0; i < inputs.Length; ++i)
            {
                mActivation += inputs[i] * mWeights[i];
            }

            return 2.0f / (1.0f + Math.Exp((-mActivation) * LAMBDA)) - 1.0f;
        }

        public double ActivationDerivative
        {
            get
            {
                double expmlx = Math.Exp(LAMBDA * mActivation);
                return 2 * LAMBDA * expmlx / ((1 + expmlx) * (1 + expmlx));
            }
        }

        public double[] SynapticWeights
        {
            get
            {
                return mWeights;
            }
        }

        public static double LAMBDA = 1.5;
    }
}

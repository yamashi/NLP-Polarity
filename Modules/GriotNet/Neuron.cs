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
                mWeights[i] = Randomizer.RandNumber();
        }

        public double Activate(double[] inputs)
        {
            mActivation = 0.0;

            for(var i = 0; i < inputs.Length; ++i)
            {
                mActivation += inputs[i] * mWeights[i];
            }

            return Math.Tanh(mActivation);
        }

        public double ActivationDerivative
        {
            get
            {
                return 1 - Math.Pow(Math.Tan(mActivation), 2.0);
            }
        }

        public double[] SynapticWeights
        {
            get
            {
                return mWeights;
            }
        }
    }
}

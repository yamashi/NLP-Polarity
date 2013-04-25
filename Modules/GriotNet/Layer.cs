using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GriotNet
{
    public class Layer
    {
        private UInt32 mNeuronCount;
        private UInt32 mDendrites;

        private List<Neuron> mNeurons = new List<Neuron>();
        private double[] mOutputs;

        public static double[] AddBias(double[] inputs)
	    {
		    double[] outputs = new double[inputs.Length + 1];
		    for (int i = 0; i < inputs.Length; ++i)
			    outputs[i + 1] = inputs[i];
		    outputs[0] = 1.0;
            return outputs;
	    }

        public Layer(UInt32 pDendrites, UInt32 pNeuronCount)
        {
            mDendrites = pDendrites + 1;
            mNeuronCount = pNeuronCount + 1;

            mOutputs = new double[mNeuronCount];

            Console.WriteLine("Layer created with {0} dendrites per neuron and {1} neurons.", pDendrites, pNeuronCount);

            for (var i = 0; i < mNeuronCount; ++i)
                mNeurons.Add(new Neuron(pDendrites));
        }

        public double[] Evaluate(double[] inputs)
        {
            double[] correctedInputs = inputs;

            if (inputs.Length != GetWeightsFor(0).Length)
                correctedInputs = AddBias(inputs);

            for (var i = 1; i < mNeuronCount; ++i)
                mOutputs[i] = mNeurons[i].Activate(inputs);

            // bias unit
            mOutputs[0] = 1.0;

            return mOutputs;
        }

        public double[] GetWeightsFor(int index)
        {
            return mNeurons[index].SynapticWeights;
        }

        public double GetActivationDerivativeFor(int index)
        {
            return mNeurons[index].ActivationDerivative;
        }

        public UInt32 Size
        {
            get
            {
                return mNeuronCount;
            }
        }

        public double[] Outputs
        {
            get
            {
                return mOutputs;
            }
        }
    }
}

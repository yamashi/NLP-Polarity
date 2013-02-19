using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic
{
    class Perceptron
    {
        public static readonly float LearningRate = 0.1f;

        private uint size;
        private float[] weights;

        public uint Size
        {
            get { return size - 1; }
        }

        public Perceptron(uint size)
        {
            this.size = size + 1;
            weights = new float[this.size];
            for (var i = 0; i < this.size; ++i)
            {
                weights[i] = (float)Global.random.NextDouble();
            }
        }

        public double ForwardPropagate(float[] inputs)
        {
            if (inputs.Length < size)
            {
                if (inputs.Length - 1 < size)
                    throw new Exceptions.InvalidArguments("Not enough inputs");
                inputs = AddBias(inputs);
            }
            double total = 0;
            for (var i = 0; i < size; ++i)
            {
                total += inputs[i] * weights[i];
            }

            return Math.Tanh(total); 
        }

        public double NormalizeOutput(float[] inputs)
        {
            return ForwardPropagate(inputs) > 0 ? 1 : -1;
        }

        public void Learn(float[] inputs, float desiredOutput)
        {
            double output = ForwardPropagate(inputs);
            double error = desiredOutput - output;

            for (var i = 0; i < size; ++i)
            {
                weights[i] += LearningRate * (float)error * weights[i]; 
            }
        }

        private float[] AddBias(float[] inputs)
        {
            float[] ins = new float[size];
            Array.Copy(inputs, ins, inputs.Length);
            ins[size - 1] = 1.0f;
            return ins;
        }
    }
}

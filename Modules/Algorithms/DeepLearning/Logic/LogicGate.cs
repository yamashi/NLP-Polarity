using NeuroBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic
{
    public class LogicGate
    {
        GriotNet.NeuralNetwork net;
        List<double[]> examples = new List<double[]>();
        List<double[]> results = new List<double[]>();

        public LogicGate()
        {
            net = new GriotNet.NeuralNetwork();

            examples.Add(new double[] { 1.0, 1.0 }); results.Add(new double[] { -1.0 });
            examples.Add(new double[] { 1.0, -1.0 }); results.Add(new double[] { 1.0 });
            examples.Add(new double[] { -1.0, 1.0 }); results.Add(new double[] { 1.0 });
            examples.Add(new double[] { -1.0, -1.0 }); results.Add(new double[] { -1.0 });
        }

        public void BuildNetwork()
        {
            net.AddLayer(2);
            net.AddLayer(6);
            net.AddLayer(1);
        }

        public void TrainEpoche()
        {
            net.Learn(examples, results, 0.3);
        }

        public double[] EvaluateOutputs(double[] input)
        {
            return net.Evaluate(input);
        }
    }
}

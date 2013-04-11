using NeuroBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic
{
    public class LogicGate
    {
        Network _network;
        ConfigNode _node;
        BasicConfig _config;

        double[] _input = new double[2];
        double[] _desired = new double[1];
        double[] _currentOutput = new double[1];

        public LogicGate()
        {
            _node = new ConfigNode();
            _config = new BasicConfig(_node);

            _config.BiasNeuronEnable.Value = true;
            _config.LearningRate.Value = 0.3;
        }

        public void BuildNetwork()
        {
            _network = new Network(_node);
            _network.AddLayer(2);
            _network.AddLayer(1);

            _network.BindInputLayer(_input);
            _network.BindTraining(_desired); 

            _network.AutoLinkFeedforward(); 
        }

        public void TrainEpoche()
        {
            _input[0] = -5;
            _input[1] = 5;
            _desired[0] = 1;
            _network.TrainCurrentPattern(true, false);

            _input[0] = 5;
            _input[1] = -5;
            _desired[0] = 1;
            _network.TrainCurrentPattern(true, false);

            _input[0] = -5;
            _input[1] = -5;
            _desired[0] = -1;
            _network.TrainCurrentPattern(true, false);

            _input[0] = 5;
            _input[1] = 5;
            _desired[0] = -1;
            _network.TrainCurrentPattern(true, false);
        }

        public double[] EvaluateOutputs(double[] input)
        {
            double[] output = new double[1];

            Array.Copy(input, _input, input.Length);

            _network.CalculateFeedforward();
            output[0] = _network.CollectOutput()[0];

            return output;
        }

        public bool BiasNeuron
        {
            get { return _config.BiasNeuronEnable.Value; }
            set { _config.BiasNeuronEnable.Value = value; }
        }

        public Network Network
        {
            get { return _network; }
        }
    }
}

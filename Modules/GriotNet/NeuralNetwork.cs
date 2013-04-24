﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GriotNet
{
    public class NeuralNetwork
    {
        private List<Layer> mLayers = new List<Layer>();
        private List<double[,]> mWeightDelta = new List<double[,]>();
        private List<double[]> mGradient = new List<double[]>();

        public NeuralNetwork()
        {

        }

        public void AddLayer(UInt32 pNeuronCount)
        {
            int i = mLayers.Count;

            mLayers.Add(new Layer(i == 0 ? pNeuronCount : mLayers[i - 1].Size, pNeuronCount));
            mWeightDelta.Add(new double[mLayers[i].Size, mLayers[i].GetWeightsFor(0).Length]);
            mGradient.Add(new double[mLayers[i].Size]);
        }

        public double[] Evaluate(double[] inputs)
        {
            double[] outputs = new double[inputs.Length];

            for (int i = 0; i < mLayers.Count; ++i)
            {
                outputs = mLayers[i].Evaluate(inputs);
                inputs = outputs;
            }

            return outputs;
        }

        private double EvaluateError(double[] pNeuralOutput, double[] pDesiredOutput)
	    {
            double[] d = pDesiredOutput;

		    // add bias to input if necessary
            if (pDesiredOutput.Length != pNeuralOutput.Length)
                d = Layer.AddBias(pDesiredOutput);

		    double e = 0;
            for (int i = 0; i < pNeuralOutput.Length; ++i)
                e += (pNeuralOutput[i] - d[i]) * (pNeuralOutput[i] - d[i]);

		    return e;
	    }

        public double EvaluateQuadraticError(List<double[]> examples, List<double[]> results)
        {
            double e = 0;

            for (int i = 0; i < examples.Count; ++i)
            {
                e += EvaluateError(Evaluate(examples[i]), results[i]);
            }

            return e;
        }

	    private void EvaluateGradients(double[] pResults)
	    {
		    // for each neuron in each layer
		    for (int c = mLayers.Count - 1; c >= 0; --c) 
            {
			    for (int i = 0; i < mLayers[c].Size; ++i) 
                {
                    // Layer is the output
                    if (c == mLayers.Count - 1) 
                    {
					    mGradient[c][i] = 2 * (mLayers[c].Outputs[i] - pResults[0])  * mLayers[c].GetActivationDerivativeFor(i);
				    }
				    else 
                    {
					    double sum = 0;

                        // sum 
					    for (int k = 1; k < mLayers[c+1].Size; ++k)
						    sum += mLayers[c+1].GetWeightsFor(k)[i] * mGradient[c+1][k];

					    mGradient[c][i] = mLayers[c].GetActivationDerivativeFor(i) * sum;
				    }
			    }
		    }
	    }

	    private void ResetWeightsDelta()
	    {
		    // reset delta values for each weight
		    for (int c = 0; c < mLayers.Count; ++c) 
            {
			    for (int i = 0; i < mLayers[c].Size; ++i) 
                {
				    double[] weights = mLayers[c].GetWeightsFor(i);
				    for (int j = 0; j < weights.Length; ++j)
				        mWeightDelta[c][i,j] = 0;
	            }		
		    }
	    }

	    private void EvaluateWeightsDelta()
	    {
		    // evaluate delta values for each weight
		    for (int c = 1; c < mLayers.Count; ++c) 
            {
			    for (int i = 0; i < mLayers[c].Size; ++i) 
                {
				    double[] weights = mLayers[c].GetWeightsFor(i);
				    for (int j = 0; j < weights.Length; ++j)
					    mWeightDelta[c][i , j] += mGradient[c][i] * mLayers[c-1].Outputs[j];
			    }
		    }
	    }

	    private void UpdateWeights(double pLearningRate)
	    {
		    for (int c = 0; c < mLayers.Count; ++c) 
            {
			    for (int i = 0; i < mLayers[c].Size; ++i) 
                {
				    double[] weights = mLayers[c].GetWeightsFor(i);
				    for (int j = 0; j < weights.Length; ++j)
					    mLayers[c].GetWeightsFor(i)[j] = mLayers[c].GetWeightsFor(i)[j]	- (pLearningRate * mWeightDelta[c][i,j]);
	            }
		    }
	    }

        private void BatchBackPropagation(List<double[]> examples,
									  List<double[]> results,
									  double pLearningRate)
        {
	        ResetWeightsDelta();

	        for (int l = 0; l < examples.Count; ++l) {
		        Evaluate(examples[l]);
		        EvaluateGradients(results[l]);
		        EvaluateWeightsDelta();
	        }

	        UpdateWeights(pLearningRate);
        }

        public void Learn(List<double[]> examples,
                          List<double[]> results,
                          double pLearningRate)
        {
            double e = double.PositiveInfinity;

            while (e > 0.001f)
            {
                BatchBackPropagation(examples, results, pLearningRate);
                e = EvaluateQuadraticError(examples, results);
            }
        }
    }
}
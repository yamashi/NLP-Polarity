using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic;

namespace UnitTest
{
    [TestClass]
    public class DeepLearningTest
    {
        [TestMethod]
        public void WordMatrix_AddFeature()
        {
            WordMatrix matrix = new WordMatrix();
            matrix.AddFeature("Test");
            matrix.AddFeature("Other");

            Assert.AreEqual(matrix.Features.Count, 2, "Feature count is not correct");
        }

        [TestMethod]
        public void WordMatrix_Naked()
        {
            WordMatrix matrix = new WordMatrix();

            Assert.AreEqual(matrix.Features.Count, 0, "Features are not initialized correctly");
            Assert.AreEqual(matrix.Matrix.Count, 0, "Word matrix is not initialized correctly");
        }

        [TestMethod]
        public void LogicGate_TrainEvaluate()
        {
            LogicGate logic = new LogicGate();
            logic.BuildNetwork();

            for (int i = 0; i < 100; ++i)
                logic.TrainEpoche();

            Assert.AreEqual(Normalize.Do(logic.EvaluateOutputs(new double[] { -5, -5 })[0]), Normalize.Output.Sleep, "Neural network failed to learn XOR. 0 0");
            Assert.AreEqual(Normalize.Do(logic.EvaluateOutputs(new double[] { 5, 5 })[0]),   Normalize.Output.Sleep, "Neural network failed to learn XOR. 1 1");
            Assert.AreEqual(Normalize.Do(logic.EvaluateOutputs(new double[] { -5, 5 })[0]),  Normalize.Output.Fire, "Neural network failed to learn XOR. 0 1");
            Assert.AreEqual(Normalize.Do(logic.EvaluateOutputs(new double[] { 5, -5 })[0]),  Normalize.Output.Fire, "Neural network failed to learn XOR. 1 0");
        }
    }
}

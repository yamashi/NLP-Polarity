using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaturalLanguageProcessing.Polarity.Algorithms.DeepLearning.Logic;

namespace UnitTest
{
    [TestClass]
    public class DeepLearningTest
    {
        [TestMethod]
        public void LogicGate_TrainEvaluate()
        {
            LogicGate logic = new LogicGate();
            logic.BuildNetwork();

            logic.TrainEpoche();

            Assert.AreEqual(Normalize.Do(logic.EvaluateOutputs(new double[] { -1, -1 })[1]), Normalize.Output.Sleep, "Neural network failed to learn XOR. 0 0");
            Assert.AreEqual(Normalize.Do(logic.EvaluateOutputs(new double[] { 1, 1 })[1]), Normalize.Output.Sleep, "Neural network failed to learn XOR. 1 1");
            Assert.AreEqual(Normalize.Do(logic.EvaluateOutputs(new double[] { -1, 1 })[1]), Normalize.Output.Fire, "Neural network failed to learn XOR. 0 1");
            Assert.AreEqual(Normalize.Do(logic.EvaluateOutputs(new double[] { 1, -1 })[1]), Normalize.Output.Fire, "Neural network failed to learn XOR. 1 0");
        }

        [TestMethod]
        public void LogicGate_RecursiveParse()
        {
            LogicGate logic = new LogicGate();
            logic.BuildNetwork();

            logic.TrainEpoche();

            Assert.AreEqual(Normalize.Do(logic.EvaluateOutputs(new double[] { -1, -1 })[1]), Normalize.Output.Sleep, "Neural network failed to learn XOR. 0 0");
            Assert.AreEqual(Normalize.Do(logic.EvaluateOutputs(new double[] { 1, 1 })[1]), Normalize.Output.Sleep, "Neural network failed to learn XOR. 1 1");
            Assert.AreEqual(Normalize.Do(logic.EvaluateOutputs(new double[] { -1, 1 })[1]), Normalize.Output.Fire, "Neural network failed to learn XOR. 0 1");
            Assert.AreEqual(Normalize.Do(logic.EvaluateOutputs(new double[] { 1, -1 })[1]), Normalize.Output.Fire, "Neural network failed to learn XOR. 1 0");
        }
    }
}

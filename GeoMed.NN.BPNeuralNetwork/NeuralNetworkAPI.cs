using GeoMed.NN.Base.Enums;
using GeoMed.NN.BPNeuralNetwork.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.NN.BPNeuralNetwork
{
    public static class NeuralNetworkAPI
    {
        public static NNResult GetNetworkResult
            (int epochs, float errorRate, ExecutedData executedData, int hiddenCount = 1) =>
            Test(Train(new NeuralNetwork(epochs, errorRate ,  hiddenCount , NNType.BackProbagation) , executedData));


        public static NNResult GetElmanNetworkResult
           (int epochs, float errorRate, ExecutedData executedData, int hiddenCount = 1) =>
           Test(Train(new NeuralNetwork(epochs, errorRate, hiddenCount , NNType.Elman) , executedData));

        private static NNResult Test(NeuralNetwork network) =>
            NeuralNetworkOperations.GetResult(NeuralNetworkOperations.TestNN(network));

        private static NeuralNetwork Train(NeuralNetwork network  , ExecutedData executedData) =>
             NeuralNetworkOperations.TrainNN(NeuralNetworkOperations.InitialLayers(network , executedData));




    }
}

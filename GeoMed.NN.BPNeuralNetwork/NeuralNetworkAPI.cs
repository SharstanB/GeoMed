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
            (int epochs, float errorRate, ExecutedData executedData, int hiddenCount = 1, NNType nNType = NNType.BackProbagation
            ) => nNType switch
            {
                 NNType.BackProbagation =>
                  Test(Train(new NeuralNetwork(NNType.BackProbagation, epochs, errorRate, hiddenCount), executedData)),

                 NNType.Elman => Test(Train(new NeuralNetwork(NNType.Elman, epochs, errorRate, hiddenCount), executedData))
            };


        private static NNResult Test(NeuralNetwork network) =>
            NeuralNetworkOperations.GetResult(NeuralNetworkOperations.TestNN(network));

        private static NeuralNetwork Train(NeuralNetwork network  , ExecutedData executedData) =>
             NeuralNetworkOperations.TrainNN(NeuralNetworkOperations.InitialLayers(network , executedData));


        public static NeuralNetwork loadModel(NNResult nResult) => NeuralNetworkOperations.LoadDataToNetwork(nResult);

    }
}

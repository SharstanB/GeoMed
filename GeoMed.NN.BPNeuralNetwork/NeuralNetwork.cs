using GeoMed.NN.Base;
using GeoMed.NN.Base.Enums;
using GeoMed.NN.BPNeuralNetwork.Layers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeoMed.NN.BPNeuralNetwork
{
    public class NeuralNetwork
    {
        #region Properties
        public int Epochs { get; set; }

        public float ErrorRate { get; set; }

        public double NetworkError { get; set; }

        public float Alpha { get; set; } 

        public int HiddenLayerCount { get; set; }

        public InputLayer InputLayer { get; set; }

        public ElmanLayer OutputLayer { get; set; }

        public NNType NNType { get; set; }

        public List<HiddenLayer> HiddenLayers { get; set; }

        #endregion

        #region Constructer
        public NeuralNetwork(NNType nNType = NNType.BackProbagation , int epoch = 1, float errorRate = 0.001f, int hiddenLayerCount = 1
                                      , float alpha = 0.1f )
        {
            Epochs = epoch;

            HiddenLayerCount = hiddenLayerCount;

            Alpha = alpha;

            ErrorRate = errorRate;

            NNType = nNType; 

            InputLayer = new InputLayer();

            HiddenLayers = new List<HiddenLayer>();

            OutputLayer = new ElmanLayer(LayerType.Output);

        }

        #endregion

    }
}

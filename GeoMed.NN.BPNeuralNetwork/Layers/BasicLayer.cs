using GeoMed.NN.Base;
using GeoMed.NN.Base.Enums;
using GeoMed.NN.BPNeuralNetwork.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.NN.BPNeuralNetwork.Layers
{

    #region Basic_Layer 
      public class BasicLayer
    {
        #region Properties

        public LayerType LayerType { get; set; }

        public int CellsCount { get; set; } 

        public List<List<double>> Weights { get; set; }

        public ActivationFuncType ActivationFunc => LayerType == LayerType.Output
                                                    ? ActivationFuncType.Linear : ActivationFuncType.Tanh;

        #endregion

        #region Constructer
        public BasicLayer(LayerType layerType, 
            int cellsCount = 3  /* default is for input layer */ )
        {
            LayerType = layerType;
            CellsCount = cellsCount;
        }
        #endregion

      
    }
    #endregion
  
    #region Input_layer 
    public class InputLayer : BasicLayer
    {
        public NNInput Input { get; set; }

        public InputLayer(LayerType layerType = LayerType.Input)
            : base(layerType)
        {
            Input = new NNInput();
        }

    }
    #endregion

    #region Hidden_Output_layer 
    public class ElmanLayer : BasicLayer
    {
        public List<Neuron> LayerCells { get; set; }
    

        public ElmanLayer(LayerType layerType, int cellsCount = 1)
           : base(layerType , cellsCount)
        {
            LayerCells = new List<Neuron>();
        }
    }
    #endregion

}

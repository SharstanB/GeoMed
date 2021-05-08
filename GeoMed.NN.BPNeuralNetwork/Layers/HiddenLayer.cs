using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country;
using GeoMed.NN.Base.Enums;
using GeoMed.NN.BPNeuralNetwork.Basic;
using GeoMed.NN.BPNeuralNetwork.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.NN.BPNeuralNetwork
{
    public  class HiddenLayer : ElmanLayer
    {
        #region Properties

        /// <summary>
        /// List of Context Layer for each Hidden Layer
        /// </summary>
        public List<ElmanLayer> HiddenContextLayer { get; set; }
       
        public int HiddenContextLayersCount { get; set; }

        #endregion

        #region Constructer
        public HiddenLayer(LayerType layerType
           , int cellsCount = 1 )
         : base(layerType, cellsCount)
        {

            var contextLayerCount = new Random().Next(1, 3);

            HiddenContextLayersCount = contextLayerCount;

            HiddenContextLayer = new List<ElmanLayer>();

            for (int i = 0; i < contextLayerCount; i++)
            {
                var layer = new ElmanLayer(LayerType.Context, cellsCount);
                for (int j = 0; j < cellsCount; j++)
                {
                    layer.LayerCells.Add(new Neuron());
                }
                HiddenContextLayer.Add(layer);
            }
        }
        #endregion

    }
}

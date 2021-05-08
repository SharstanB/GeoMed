using GeoMed.NN.Base;
using GeoMed.NN.Base.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.NN.BPNeuralNetwork.Basic
{
    public class NNResult 
    {
        public List<SampleResult> TestSamples { get; set; }

        public List<(List<SampleResult> samples , int epoch )> TrainSamples { get; set; }

        public List<FinalWeigth> FinalWeigths { get; set; }

        public double NetworkError { get; set; }

        public NNResult()
        {
            FinalWeigths = new List<FinalWeigth>();

            TrainSamples = new List<(List<SampleResult> samples, int epoch)>();

            TestSamples = new List<SampleResult>();
        }
    }

    public class SampleResult
    {
        public double ActualOutput { get; set; }

        public double TargetOutput { get; set; }

        public double NeuronError =>  TargetOutput - ActualOutput ;
    }

    /// <summary>
    /// Finale Weigths should save as Trained Model to calc new future values 
    /// </summary>
    public class FinalWeigth
    {
        public int HiddenNumber { get; set; }

        /// <summary>
        /// if current layer is hidden should numeric this hidden layer because may be
        /// we have multi hidden layers
        /// </summary>
        private LayerType _LayerType;
        public LayerType LayerType 
        { 
            get { return _LayerType; } 
            set 
            {
                _LayerType = value;
                if(_LayerType == LayerType.Hidden)
                {
                    ++HiddenNumber;
                }
            } 
        }
        public List<List<double>> weigths { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.NN.BPNeuralNetwork.Basic
{
     public class Neuron
    {
        public double value { get; set; }

        public Nullable<double> Error { get; set; }
    }
}

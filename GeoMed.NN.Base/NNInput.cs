using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.NN.Base
{
    public class NNInput
    {
        public double Population { get; set; }

        public double MedianAge { get; set; }

        public double Cases { get; set; }

        public double TargetOutput { get; set; } // get output target for each sample

        public string Date { get; set; }
    }
}

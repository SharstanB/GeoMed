using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.NN.Base.Enums
{
    public enum NNType
    {
        /// <summary>
        ///  multiple layers neural network
        /// </summary>
        BackProbagation ,
        /// <summary>
        ///  RNN network (with context layer for each hidden layer)
        /// </summary>
        Elman,
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.NN.Base.Enums
{
    /// <summary>
    /// Neural Network Layer Types 
    /// </summary>
    public enum LayerType
    {
        /// <summary>
        /// For all NN types
        /// </summary>
        Input,

        /// <summary>
        /// For all NN types
        /// </summary>
        Output,

        /// <summary>
        /// For all NN types
        /// </summary>
        Hidden,

        /// <summary>
        /// For Elman NN
        /// </summary>
        Context ,

        /// <summary>
        /// For CNN NN
        /// </summary>
        Conv,

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.NN.Base.Enums
{
    public enum ExecutedData
    {
        /// <summary>
        /// when NN input is US states
        /// </summary>
        State ,

        /// <summary>
        /// when NN input is US counties
        /// </summary>
        County,

        /// <summary>
        /// when train and validate date is same  (dependence on training mey
        /// be if vaidate and train is same =>  accuracy be high)
        /// </summary>
        all,
        /// <summary>
        /// when train and validate date is splited  (dependence on training mey
        /// be if vaidate and train is deffirent =>  accuracy be high)
        /// </summary>
        splited
    }
}

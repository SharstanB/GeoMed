using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.NN.Base
{
    public static class ErrorFunctions
    {
        public static double ClacErrorHelper( double output)
        {
            return output * (1 - output); 
        }

        /// <summary>
        ///  Network error is defference between target output and actual
        /// </summary>
        /// <param name="sample">get sample target output  </param>
        /// <param name="newOutput"> actual output for this sample (list to process more than one output) </param>
        /// <returns></returns>
        public static (double ErrorObtained, double NetworkError) NNPBError(this NNInput sample , List<double> newOutput)
        {
            var errorRes = sample.TargetOutput - newOutput.FirstOrDefault();
            return (Math.Pow(errorRes , 2) , errorRes  ); 
        }
        /// <summary>
        /// calculate network mean squared error to test network preformance
        /// </summary>
        /// <param name="sourse">acollection of pairs of outputs and targets of samples </param>
        /// <returns></returns>
        public static double MeanSquaredError(this IEnumerable<(double target , double output )> sourse)
        {
            return sourse.Sum(s => Math.Pow(s.target - s.output, 2)) / sourse.Count();
        }
    }
}

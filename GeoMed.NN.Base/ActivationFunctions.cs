using System;

namespace GeoMed.NN.Base
{
    public static class ActivationFunctions
    {
        /// <summary>
        /// Applies the sigmoid function, 1 / (1 + e^(-x))
        /// </summary>
        /// <param name="x">The input to process</param>
        public static double Sigmoid(double x) => 1 / (1 + (double)Math.Exp(-x));

        /// <summary>
        /// Applies the sigmoid prime function, 1 / (1 + e^(-x))
        /// </summary>
        /// <param name="x">The input to process</param>
        public static double SigmoidPrime(double x)
        {
            double
                exp = (double)Math.Exp(x),
                sum = 1 + exp,
                square = sum * sum,
                div = exp / square;
            return div;
        }

        /// <summary>
        /// Applies the tanh function
        /// </summary>
        /// <param name="x">The input to process</param>
        public static double Tanh(double x)
        {
            var res = Math.Exp(-x) /  Math.Pow((1 + Math.Exp(-x)), 2) ;
            return res;
        }

        /// <summary>
        /// Applies the rectifier function
        /// </summary>
        /// <param name="x">The input to process</param>
        public static double ReLU(double x) => x > 0 ? x : 0;

        /// <summary>
        /// Applies the derivative of the <see cref="Tanh"/> function
        /// </summary>
        /// <param name="x">The input to process</param>
        /// <remarks>The real derivative is indetermined when x is 0</remarks>
        public static double ReLUPrime(double x) => x <= 0 ? 0 : 1;

        /// <summary>
        /// Applies the leaky ReLU function
        /// </summary>
        /// <param name="x">The input to process</param>
        public static double LeakyReLU(double x) => x > 0 ? x : 0.01f * x;

        /// <summary>
        /// Applies the derivative of the <see cref="LeakyReLU"/> function
        /// </summary>
        /// <param name="x">The input to process</param>
        /// <remarks>The real derivative is indetermined when x is 0</remarks>
        public static double LeakyReLUPrime(double x) => x > 0 ? 1 : 0.01f;

        /// <summary>
        /// Applies the the numerator part of the softmax activation function, e^x
        /// </summary>
        /// <param name="x">The input to process</param>
        /// <remarks>The derivative is not available, as it doesn't appear in the derivative of the log-likelyhood cost function</remarks>
        public static double Softmax(double x) => (double)Math.Exp(x);

        /// <summary>
        /// Applies the softplus function, ln(1 + e^x)
        /// </summary>
        /// <param name="x">The input to process</param>
        /// <remarks>The derivative of the softplus is the <see cref="Sigmoid"/> function</remarks>
        public static double Softplus(double x)
        {
            double
                exp = (double)Math.Exp(x),
                sum = 1 + exp,
                ln = (double)Math.Log(sum);
            return ln;
        }

        /// <summary>
        /// Applies the exponential linear unit function
        /// </summary>
        /// <param name="x">The input to process</param>
        public static double ELU(double x) => x >= 0 ? x : (double)Math.Exp(x) - 1;

        /// <summary>
        /// Applies the derivative of the <see cref="ELU"/> function
        /// </summary>
        /// <param name="x">The input to process</param>
        public static double ELUPrime(double x) => x >= 0 ? 1 : (double)Math.Exp(x);

        /// <summary>
        /// Applies the absolute ReLU linear unit function
        /// </summary>
        /// <param name="x">The input to process</param>
        public static double AbsoluteReLU(double x) => x >= 0 ? x : -x;

        /// <summary>
        /// Applies the derivative of the <see cref="AbsoluteReLU"/> function
        /// </summary>
        /// <param name="x">The input to process</param>
        public static double AbsoluteReLUPrime(double x) => x >= 0 ? 1 : -1;

        /// <summary>
        /// Applies the Linear function
        /// </summary>
        /// <param name="x">The input to process</param>
        public static double Linear(double x) => x;

        /// <summary>
        /// Applies the derivative of the <see cref="Linear"/> function
        /// </summary>
        /// <param name="x">The input to process</param>
        public static double Linearprime(double x) => 1;
    }
}

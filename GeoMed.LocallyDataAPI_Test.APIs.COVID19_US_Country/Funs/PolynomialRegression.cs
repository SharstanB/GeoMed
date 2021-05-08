using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country.Funs
{
    /// <summary>
    ///  go to <see cref="https://rosettacode.org/wiki/Polynomial_regression#C.2B.2B"/> 
    /// </summary>
    internal static class PolynomialRegression
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="degree"> if equal -1 set [array.length-1] </param>
        /// <returns></returns>
        public static double[] Polyfit(this (double[] x, double[] y) points, int degree=-1)
        {
            if (degree > points.x.Length-1 || points.x.Length != points.y.Length)
                throw new Exception("check length of points array and degree");



            if (degree==-1)
                degree = points.x.Length - 1;

            degree=degree>60?9:degree;
            //test
            //for (int i = 0; i < points.x.Length; i++)
            //{
            //    Debug.WriteLine("{0} {1}", points.x[i], points.y[i]);
            //}
            //points.x  = new[] { 0.0, 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0 };
            //points.y = new[] { 1.0, 6.0, 17.0, 34.0, 57.0, 86.0, 121.0, 162.0, 209.0, 262.0, 321.0 };
            //degree = 10;

            // Vandermonde matrix
            var v = new DenseMatrix(points.x.Length, degree + 1);
            for (int i = 0; i < v.RowCount; i++)
                for (int j = 0; j <= degree; j++) v[i, j] = Math.Pow(points.x[i], j);
            var yv = new DenseVector(points.y).ToColumnMatrix();
            MathNet.Numerics.LinearAlgebra.Factorization.QR<double> qr = v.QR();
            // Math.Net doesn't have an "economy" QR, so:
            // cut R short to square upper triangle, then recompute Q
            var r = qr.R.SubMatrix(0, degree + 1, 0, degree + 1);
            var q = v.Multiply(r.Inverse());
            var p = r.Inverse().Multiply(q.TransposeThisAndMultiply(yv));
            return p.Column(0).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="polyfit"></param>
        /// <returns></returns>
        public static (double[] evaluate, double[] diff) GetEvaluateDiff(this (double[] x, double[] y) points , double[] polyfit )
        {
            double[] leftside = new double[points.x.Length], rightside = new double[points.x.Length];

            for (int i = 0; i < points.x.Length; i++)
            {
                var evaluate = Polynomial.Evaluate(points.x[i], polyfit);
                var diff = points.y[i] - evaluate;

                leftside[i] = evaluate;
                rightside[i] = diff;
            }
            return (leftside, rightside);
        }

    }
}

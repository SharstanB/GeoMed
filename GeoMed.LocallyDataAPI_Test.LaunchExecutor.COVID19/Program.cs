using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country;
using System;
using System.Collections.Generic;

namespace GeoMed.LocallyDataAPI_Test.LaunchExecutor.COVID19
{
    class Program
    {
        static void Main(string[] args)
        {

          //  string equation =COVID19USCountry.ToStringPolynomial(COVID19USCountry.GetPolynomialCases("Gwinnett"));

          //  var equation = COVID19USCountry.GetDataInput(NN.Base.Enums.ExecutedData.County);

         var result = COVID19USCountry.GetCountiesLSTMInput<List<float[]>>();

            //  Console.WriteLine(equation);

            //GA Georgia Gwinnett
           // Console.WriteLine(result);

            Console.WriteLine("Done!");

        }
    }
}

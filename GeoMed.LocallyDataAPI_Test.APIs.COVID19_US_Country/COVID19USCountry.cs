using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country.Filters;
using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country.Funs;
using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country.IO;
using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country.Models;
using GeoMed.NN.Base;
using GeoMed.NN.Base.Enums;
using GeoMed.NN.Base.LSTMDTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country
{
    /// <summary>
    /// Class must use and enable Evaluate 
    /// </summary>
    public class COVID19USCountry
    {
        /// <summary>
        /// مجلد سطح المجلد بسمى مشروع التخرج يحوي مجلد داتا ست المرض
        /// </summary>
        private static string diseaseDataset_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "مشروع التخرج", "diseaseDataset", "covid_us_county.csv");

        /// <summary>
        ///  مجلد سطح المجلد بسمى مشروع التخرج يحوي مجلد داتا ست المعلومات
        /// </summary>
        private static string  usInfoDataset_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "مشروع التخرج", "usInfoDataset", "us_county.csv");

        /// <summary>
        /// get Polynomial equation  cases for custome read as Georgia  
        /// </summary>
        /// <returns></returns>
        public static double[] GetPolynomialCases(string statecode) => diseaseDataset_path.ReadToEnd(statecode).Difference().ToDateAndCases().Polyfit();


        /// <summary>
        /// get Polynomial equation  deaths for custome read as Georgia  
        /// </summary>
        /// <returns></returns>
        public static double[] GetPolynomialDeaths(string statecode) => diseaseDataset_path.ReadToEnd(statecode).Difference().ToDateAndDeaths().Polyfit();


        /// <summary>
        /// get Elman Neural Network Input 
        /// </summary>
        /// <returns></returns>
        public static (IEnumerable<NNInput> trainData, IEnumerable<NNInput> testData) GetDataInput(ExecutedData executedData) => executedData == ExecutedData.State ?
            (diseaseDataset_path , usInfoDataset_path).ReadStatesInput() : (diseaseDataset_path, usInfoDataset_path).ReadCountiesInput();


        /// <summary>
        /// get spliting Data (trainx, trainY) for LSTM Neural Network Input 
        /// </summary>
        /// <returns></returns>
        public static (IEnumerable<Sample> trainData, IEnumerable<Sample> testData) GetCountiesLSTMInputWithSplit() =>
            (diseaseDataset_path, usInfoDataset_path).ReadCountiesLSTMInputWithSplit();

        /// <summary>
        /// get Elman Neural Network Input 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<T> GetCountiesLSTMInput<T>() =>
            (diseaseDataset_path, usInfoDataset_path).ReadCountiesLSTMInput<T>();


        /// <summary>
        ///  get Polynomial formal equation  c0 * X^0 + c1 * X^1 + c2 * X^2 ... cn * X^n
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string ToStringPolynomial(double[] d )
        {
            string s=String.Empty;
            int iter = 0;
            foreach (var item in d)
                s += item + $" * X^{iter++} + ";

            return s.Trim(' ').Trim('+');
        }


        public static IEnumerable<DiseaseInfoModel> USAAggregate() => Reader.Select<DiseaseInfoModel>(diseaseDataset_path).USACombine();

        public static IEnumerable<DiseaseInfoModel> ALLUSA() => Reader.Select<DiseaseInfoModel>(diseaseDataset_path).ToList().Difference();

        public static IEnumerable<USInfoModel> ALLUSAInfo() => Reader.Select<USInfoModel>(usInfoDataset_path);


    }
}

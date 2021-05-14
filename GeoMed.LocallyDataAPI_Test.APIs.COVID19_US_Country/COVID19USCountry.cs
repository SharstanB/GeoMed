﻿using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country.Filters;
using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country.Funs;
using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country.IO;
using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country.Models;
using GeoMed.NN.Base;
using GeoMed.NN.Base.Enums;
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
        public static (List<NNInput> trainData, List<NNInput> testData) GetStateInput(ExecutedData executedData) => executedData == ExecutedData.State ?
            (diseaseDataset_path , usInfoDataset_path).ReadStatesInput() : (diseaseDataset_path, usInfoDataset_path).ReadCountiesInput();


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


        /// <summary>
        /// مجلد سطح المجلد بسمى مشروع التخرج يحوي مجلد الداتا ست
        /// </summary>
        private static string dataset_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "مشروع التخرج", "dataset", "covid_us_county.csv");

        public static IEnumerable<DiseaseInfoModel> USAAggregate() => Reader.Select<DiseaseInfoModel>(dataset_path).USACombine();

    }
}
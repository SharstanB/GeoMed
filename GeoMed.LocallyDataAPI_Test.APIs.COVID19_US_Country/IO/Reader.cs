using CsvHelper;
using CsvHelper.Configuration;
using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country.Filters;
using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country.Models;
using GeoMed.NN.Base;
using GeoMed.NN.Base.LSTMDTOs;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country.IO
{
    /// <summary>
    /// reader fast and easy with huge data csv  look for CsvHelper and more in github repos
    /// </summary>
    public static class Reader
    {

        private static string ModelPath = "wwwroot/models";
        /// <summary>
        /// 
        /// </summary>
        private static int getMaxAllowCount = (int)Math.Sqrt(int.MaxValue) - 100;


        public  static IEnumerable<T> Select<T>(string csvpath)
        {
            if (!csvpath.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                throw new Exception("should be csv file");

            Console.WriteLine("please waiting... until csvpath read");
            // const int saveCount = 200;
            List<T> data;

            using (var reader = new StreamReader(csvpath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                data =  csv.GetRecords<T>().ToList();
                //Georgia  ,Pike  GA   
            }
            return data;
        }

        public static void Write<T>(IEnumerable<T> source)
        {
            if (!Directory.Exists(ModelPath))
            {
                Directory.CreateDirectory(ModelPath);
            }
            using (var writer = new StreamWriter($"{ModelPath}\\file.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(source);
            }
        }

        /// <summary>
        /// easy method to read 
        /// <para>slow reader effect using linq with cached on memory </para>
        /// </summary>
        /// <param name="csvpath"></param>
        /// <param name="country"></param>
        /// <param name="saveMemory">data will burn your laptop</param>
        /// <returns></returns>
        public static List<DiseaseInfoModel> ReadToEnd(this string csvpath , string country , bool saveMemory=false)
        {
            const int saveCount= 200;
            List<DiseaseInfoModel> data = Select<DiseaseInfoModel>(csvpath)
                   .Where(x => x.Country == country).
                    OrderBy(x => x.Date). // order from begining
                    SkipWhile(x => x.Cases == 0).// Skip all data which covid does not effect state 
                    Take(saveMemory ? saveCount : getMaxAllowCount)
                   .ToList(); 
          
            const int longof = 100;
            int miniCount = (int)Math.Sqrt(int.MaxValue) - longof;
            int _count = data.Count;

            //...DenseMatrix
            if (_count > miniCount)
            {
                //data will burn your laptop
                data.RemoveRange(miniCount, data.Count - miniCount);

                Console.WriteLine("cut data for DenseMatrix count*count over int.MaxValue  {0}*{0}  to be {1}*{1} ", _count, miniCount);
            }
            //46,340


            return data;
        }


        public static ( List<NNInput> trainData , List<NNInput> testData ) ReadStatesInput(this (string diseaseInfoPath, string usInfoPath ) paths, bool saveMemory = false)
        {
            const int saveCount = 200;

            var fList = Select<DiseaseInfoModel>(paths.diseaseInfoPath).ToList().Difference()
                   .GroupBy(h => new { h.StateCode  , h.Date.Month}).Select(item => new {
                       
                     date = item.Key.Month,

                     StateCode = item.Key.StateCode,

                      /* Average casualties for each state in a given month 
                       to the total casualties of the month */
                     Cases = item.GroupBy(g=>g.Country) 
                     .Sum(s=> s.LastOrDefault().Cases - s.FirstOrDefault().Cases) 
                     / item.Sum(s=> s.Cases),

                   })
                   .Take(saveMemory ? saveCount : getMaxAllowCount)
                   .OrderBy(o => o.date).ToList();

            var UsInfoList = Select<USInfoModel>(paths.usInfoPath);
            var USPopulationCount = UsInfoList.Sum(s=>s.Population);
            var sList = UsInfoList
                    .GroupBy(group => group.StateCode).Select(g=> new USInfoModel()
                    {
                       MedianAge = g.Sum(f=>f.MedianAge)
                         / g.Count(),

                       Population = g.Sum(f=>f.Population)
                       / USPopulationCount,

                       StateCode = g.Key,
                    })
                    .Take(saveMemory ? saveCount : getMaxAllowCount);


            var data = fList.Join(sList
                 , s => s.StateCode,
                 f => f.StateCode,
                 (a, b) => new { a , b }).Select((item , index) => new NNInput
                 {
                     Cases = item.a.Cases,
                     MedianAge = item.b.MedianAge,
                     Population = item.b.Population,
                     TargetOutput = (index + 1 < fList.Count()) ? fList[index + 1].Cases : fList[index].Cases
                 }).ToList();


            Write(data);
            var testInputs = data.TakePercent(25 , 75).ToList();

            var trainInputs = data.TakePercent(75 , 0 ).ToList();

            return (trainInputs , testInputs);
        }


        public static (List<NNInput> trainData, List<NNInput> testData) ReadCountiesInput(this (string diseaseInfoPath, string usInfoPath) paths, bool saveMemory = false)
        {
            const int saveCount = 1000;

            var usDiseaseInfo = Select<DiseaseInfoModel>(paths.diseaseInfoPath);
            var usCases = usDiseaseInfo.Sum(s => s.Cases);
            var fList = usDiseaseInfo
                   .GroupBy(data => new { data.Date.Month, data.FipsCode })
                   .Select(item => new
                    {

                        date = item.Select(s => s.Date).FirstOrDefault(),

                        fips = item.Key.FipsCode,

                        Cases = item.Sum(s => s.Cases)
                       / usCases,

                    })
                   .Take(saveMemory ? saveCount : getMaxAllowCount)
                   .OrderBy(o => o.date).ToList();

            var UsInfoList = Select<USInfoModel>(paths.usInfoPath);
            var USPopulationCount = UsInfoList.Sum(s => s.Population);
            var USAgesCount = UsInfoList.Sum(s => s.MedianAge);
            var sList = UsInfoList
                    .Select(g => new USInfoModel()
                    {
                        MedianAge = g.MedianAge
                        / USAgesCount,

                        Population = g.Population
                        / USPopulationCount,

                        FipsCode = g.FipsCode,
                    })
            .Take(saveMemory ? saveCount : getMaxAllowCount);


            var data = fList.Join(sList
                 , s => s.fips,
                 f => f.FipsCode,
                 (a, b) => new { a, b })
                .Select((item, index) => new NNInput
                {
                    Cases = item.a.Cases,
                    MedianAge = item.b.MedianAge,
                    Population = item.b.Population,
                    TargetOutput = (index + 1 < fList.Count()) ? fList[index + 1].Cases : fList[index].Cases,
                    // Date = item.a.date.ToString("MM-dd-yyyy")
                }).ToList();
            Write(data);

            var testInputs = data.TakePercent(25 , 75).ToList();

            var trainInputs = data.TakePercent(75 , 0 ).ToList();

            return (trainInputs, testInputs);

           // return (new List<NNInput>() , new List<NNInput>());
        }


        public static (IEnumerable<Sample> trainData, IEnumerable<Sample> testData
            ) ReadCountiesLSTMInputWithSplit(this (string diseaseInfoPath, string usInfoPath) paths, bool saveMemory = false)
        {
            const int saveCount = 1000;

            var usDiseaseInfo = Select<DiseaseInfoModel>(paths.diseaseInfoPath);

            var usCases = usDiseaseInfo.Sum(s => s.Cases);
            var fList = usDiseaseInfo
                   .GroupBy(data => new { data.Date, data.FipsCode })
                   .Select(item => new
                   {

                       date = item.Key,

                       fips = item.Key.FipsCode,

                       Cases = item.Sum(s => s.Cases)
                       / usCases,

                   })
                   .Take(saveMemory ? saveCount : getMaxAllowCount)
                    //.OrderBy(o => o.date)
                   .ToList();

            var UsInfoList = Select<USInfoModel>(paths.usInfoPath);
            var USPopulationCount = UsInfoList.Sum(s => s.Population);
            var USAgesCount = UsInfoList.Sum(s => s.MedianAge);
            var sList = UsInfoList
                    .Select(g => new USInfoModel()
                    {
                        MedianAge = g.MedianAge
                        / USAgesCount,

                        Population = g.Population
                        / USPopulationCount,

                        FipsCode = g.FipsCode,
                    })
            .Take(saveMemory ? saveCount : getMaxAllowCount);


            var Data = fList.Join(sList
                 , s => s.fips,
                 f => f.FipsCode,
                 (a, b) => new { a, b })
                .Select((item, index) => new
                {
                    Date = item.a.date,
                    Cases = item.a.Cases,
                    MedianAge = item.b.MedianAge,
                    Population = item.b.Population,
                }).ToList();

           var finalTrainData = Data.TakePercent(50 , 0).GroupBy(s=>s.Date.Date)
                .Select(item => new Sample() {
                    Date = item.Key,
                    Features = item.Select(feature => new Feature()
                    {
                        Cases = feature.Cases,
                        //MedianAge = feature.MedianAge,
                        //Population = feature.Population
                    }).ToList()
                })
                .ToList();

            var finalTestData = Data.TakePercent(50, 50).GroupBy(s => s.Date.Date)
                .Select(item => new Sample()
                {
                    Date = item.Key,
                    Features = item.Select(feature => new Feature()
                    {
                        Cases = feature.Cases,
                        //MedianAge = feature.MedianAge,
                        //Population = feature.Population
                    }).ToList()
                })
                .ToList();

            //var allData = Data.TakePercent(50, 50).GroupBy(s => s.Date.Date)
            //   .Select(item => new Sample()
            //   {
            //       Date = item.Key,
            //       Features = item.Select(feature => new Feature()
            //       {
            //           Cases = feature.Cases,
            //           //MedianAge = feature.MedianAge,
            //           //Population = feature.Population
            //       }).ToList()
            //   })
            //   .ToList();

            return (finalTrainData, finalTestData);
        }


        public static IList<T>  ReadCountiesLSTMInput<T> (this (string diseaseInfoPath, string usInfoPath) paths, bool saveMemory = false)
        {
            const int saveCount = 10000;

            var usDiseaseInfo = Select<DiseaseInfoModel>(paths.diseaseInfoPath);

            var usCases = usDiseaseInfo.Sum(s => s.Cases);

            //var countFib = usDiseaseInfo.Select(s => s.FipsCode).Distinct();
            var fList = usDiseaseInfo.ToList()
                 .Difference()
                .Where(a=>a.Cases >= 0)
                   .GroupBy(data => new
                   {
                       data.FipsCode,
                       data.Date
                   })
                   .Select(item => new
                   {

                       date = item.Key.Date,

                       fips = item.Key.FipsCode,

                       Cases = item.FirstOrDefault().Cases
                    //  / usCases,

                   })
                   .Take(saveMemory ? saveCount : getMaxAllowCount)
                   //.OrderBy(o => o.date)
                   .ToList();

            var UsInfoList = Select<USInfoModel>(paths.usInfoPath);
            var USPopulationCount = UsInfoList.Sum(s => s.Population);
            var USAgesCount = UsInfoList.Sum(s => s.MedianAge);
            var sList = UsInfoList
                    .Select(g => new USInfoModel()
                    {
                        MedianAge = g.MedianAge,
                      //  / USAgesCount,

                        Population = g.Population,
                    //    / USPopulationCount,

                        FipsCode = g.FipsCode,
                    })
            .Take(saveMemory ? saveCount : getMaxAllowCount);


            var Data = fList.Join(sList
                 , s => s.fips,
                 f => f.FipsCode,
                 (a, b) => new { a, b })
                .Select((item, index) => new 
                {
                    Date = item.a.date,
                    Cases = item.a.Cases,
                    MedianAge = item.b.MedianAge,
                    Population = item.b.Population,
                    Code = item.b.FipsCode,
                }).ToList();

            if(typeof(T) == typeof(Sample))
            {
                return (IList<T>) Data.GroupBy(s => s.Code
                //new { s.Code , s.Date.Date }
                )
             .Select(item => new Sample()
             {
                // Date = item.Key.Date,
                 Code = item.Key,
                 Features = item.Select(feature => new Feature()
                 {
                     Cases = feature.Cases,
                     MedianAge = feature.MedianAge,
                     Population = feature.Population
                 }).ToList()
             })
             .ToList();
            }

            var allData2 = Data.GroupBy(s => new { s.Code })
               .Select(item => item.Select(feature => new float[] { (float)(feature.Cases) }).ToList())
               .ToList();
            var allData = Data.GroupBy(s => new { s.Code })
               .Select(item => item.Select(feature => new float[] { (float)((feature.Cases - item.Min(a=>a.Cases))
               /(item.Max(a=>a.Cases - item.Min(a=>a.Cases))))}).ToList())
               .ToList();
            //var allData = Data.GroupBy(s => new { s.Code })
            //   .Select(item => item.Select(feature => new float[] { (float)(feature.Cases)}).ToList())
            //   .ToList();
            ////var ff = allData.Select(item => item.Select(d => d.FirstOrDefault()));
            //  Write(ff);
            var list = new List<List<float[]>>();
            allData.TakePercent(80 , 0).ToList().ForEach(item =>
            {
                list.Add(item.Take(201).ToList());
                list.Add(item.TakeLast(201).ToList());
            });


            return (IList<T>)list; 
        }





    }
}

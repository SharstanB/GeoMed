using CsvHelper;
using CsvHelper.Configuration;
using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country.Models;
using GeoMed.NN.Base;
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
    internal static class Reader
    {


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

            var fList = Select<DiseaseInfoModel>(paths.diseaseInfoPath)
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

            var testInputs = data.TakePercent(25).ToList();

            var trainInputs = data.TakePercent(75).ToList();

            return (trainInputs , testInputs);
        }


        public static (List<NNInput> trainData, List<NNInput> testData) ReadCountiesInput(this (string diseaseInfoPath, string usInfoPath) paths, bool saveMemory = false)
        {
            const int saveCount = 200;

            var fList = Select<DiseaseInfoModel>(paths.diseaseInfoPath)
                   .GroupBy(data => new { data.Date.Month , data.Country })
                   .Select(item => new 
                   {

                       date = item.Key.Month,

                       country = $"{item.Key.Country} County",

                       Cases = item.Sum(s=>s.Cases) ,

                   })
                   .Take(saveMemory ? saveCount : getMaxAllowCount)
                   .OrderBy(o => o.date).ToList();

            var UsInfoList = Select<USInfoModel>(paths.usInfoPath);
          //  var USPopulationCount = UsInfoList.Sum(s => s.Population);
            var sList = UsInfoList
                    .Select(g => new USInfoModel()
                    {
                        MedianAge = g.MedianAge,

                        Population = g.Population ,

                        County = g.County,
                    });
            // .Take(saveMemory ? saveCount : getMaxAllowCount);


            var data = fList.Join(sList
                 , s =>  s.country,
                 f => f.County,
                 (a, b) => new { a , b })
                .Select((item, index) => new NNInput
                {
                    Cases = item.a.Cases,
                    MedianAge = item.b.MedianAge,
                    Population = item.b.Population,
                    TargetOutput = (index + 1 < fList.Count()) ? fList[index + 1].Cases : fList[index].Cases
                }).ToList();
           

            var testInputs = data.TakePercent(25).ToList();

            var trainInputs = data.TakePercent(75).ToList();

              return (trainInputs, testInputs);
        }


    }
}

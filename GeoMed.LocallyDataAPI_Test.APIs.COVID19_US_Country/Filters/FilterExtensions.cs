using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country.Filters
{
    /// <summary>
    ///  custome select on data
    /// </summary>
    internal static class  FilterExtensions
    {
        /// <summary>
        /// Difference between tow rows ordering
        /// </summary>
        /// <param name="list"></param>
        /// <returns>new list of <see cref="DiseaseInfoModel"/></returns>
        public static List<DiseaseInfoModel> Difference(this List<DiseaseInfoModel> list)
        {
            List<DiseaseInfoModel> data = new List<DiseaseInfoModel>(list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                if (i == 0)
                {
                    data.Add(list[i]);
                    continue;
                }
                var pervious = list[i - 1];
                var current = list[i].Copy();

                current.Cases -= pervious.Cases;
                current.Deaths -= pervious.Deaths;
                data.Add(current);
            }
            list.Clear();
            return data;
        }



        public static IEnumerable<DiseaseInfoModel> USACombine(this IEnumerable<DiseaseInfoModel> list)
        {
            return list.GroupBy(model=>(model.State, model.Country)).Select(group=> new DiseaseInfoModel() {
                //Cases=x.Aggregate(0d,(all,next)=> all+=next.Cases),
                //Deaths=x.Aggregate(0,(all,next)=> all+=next.Deaths),
                Cases=group.Sum(x=>x.Cases),
                Deaths=group.Sum(x => x.Deaths),
                Country = group.Key.Country, 
                State= group.Key.State,
                StateCode =group.First().StateCode,
                Lat = group.First().Lat,
                Long = group.First().Long,
                // FipsCode
            }).ToList();
        }



        public static (double[] x, double[] y) ToDateAndCases(this List<DiseaseInfoModel> list)
        {
            double[] x = Enumerable.Range(1, list.Count).Select(Convert.ToDouble).ToArray();
            double[] y = list.Select(x => (double)x.Cases).ToArray();
            list.Clear();
            return (x, y);
        }

        public static (double[] x, double[] y) ToDateAndDeaths(this List<DiseaseInfoModel> list)
        {
            double[] x = Enumerable.Range(1, list.Count).Select(Convert.ToDouble).ToArray();
            double[] y = list.Select(x => (double)x.Deaths).ToArray();
            list.Clear();
            return (x, y);
        }




    }
}

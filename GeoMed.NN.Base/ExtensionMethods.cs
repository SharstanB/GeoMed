using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.NN.Base
{
    public static class ExtensionMethods
    {
        /// <summary>
        ///  return a percentage from source collection 
        /// </summary>
        /// <typeparam name="T"> collection type </typeparam>
        /// <param name="source"> data source collection </param>
        /// <param name="percent"> returned data precent </param>
        /// <returns></returns>
        public static IEnumerable<T> TakePercent<T>(this IEnumerable<T> source, double percent , double skip)
        {
            int count = (int)(source.Count() * percent / 100);
            int skipCount = (int)(source.Count() * skip / 100);
            return source.Skip(skipCount).Take(count);
        }

        /// <summary>
        /// clone list items 
        /// </summary>
        /// <typeparam name="T"> source collection data type </typeparam>
        /// <param name="source">cloned source collection </param>
        /// <returns></returns>
        public static List<T> GetClone<T>(this List<T> source)
        {
            return source.GetRange(0,source.Count).ToList();
        }



   

        //static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(IList<TOuter> outer, IList<TInner> inner, Func<TOuter, TInner, TResult> resultSelector)
        //{
        //    if(outer != null)
        //    {

        //        foreach (TOuter item in outer)
        //        {
        //            //Lookup<TKey, TInner>  g = lookup.GetGrouping(outerKeySelector(item), false);
        //            if (inner != null)
        //            {
        //                for (int i = 0; i < inner.Count(); i++)
        //                {
        //                    if(item.Equals(inner[i], ))
        //                    yield return resultSelector(item, inner[i]);
        //                }
        //            }
        //        }

        //    }
           
        //}
    }
}

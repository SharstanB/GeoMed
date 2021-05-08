﻿using System;
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
        public static IEnumerable<T> TakePercent<T>(this ICollection<T> source, double percent)
        {
            int count = (int)(source.Count * percent / 100);
            return source.Take(count);
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

      
    }
}

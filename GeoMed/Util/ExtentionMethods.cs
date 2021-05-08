using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoMed.Util
{
    public static class ExtentionMethods
    {
        public static string GetActionName(this string actionName)
        {
            return actionName.Replace("Controller", String.Empty);
        }

        //public static IEnumerable<> Pagination (IQueryable<> entity)
        //{

        //}
    }
}

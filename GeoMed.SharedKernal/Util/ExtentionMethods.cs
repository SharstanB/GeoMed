using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.SharedKernal.Util
{
    public static class ExtentionMethods
    {
        public static string ToBlood(this string bloodType)
        {
            if (bloodType[0] == 'P')
            {
                return bloodType.Split('_')[1] + "+";
            }
            return bloodType.Split('_')[1] + "-";
        }

    }
}

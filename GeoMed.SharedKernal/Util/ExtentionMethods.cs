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

        public static string ToformatingDate(this DateTime firstDate)
        {
            DateTime secontDate = DateTime.Now;
            var years = (secontDate - firstDate).Days / 365;
            var month = ((secontDate - firstDate).Days % 365) / 30 ;
            var days = ((secontDate - firstDate).Days % 365) % 30;


            return (years > 0 ? $"{years} سنة" : "") + (month > 0 ? $"{month} شهر" : "")
                + (days > 0 ? $"{days} يوم"  : "") ;
        }

    }
}

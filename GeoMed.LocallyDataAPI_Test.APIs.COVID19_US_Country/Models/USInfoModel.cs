using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country.Models
{
    public class USInfoModel 
    {
        [Name("population")]
        public double Population { get; set; }

        [Name("median_age")]
        public double MedianAge { get; set; }

        [Name("state_code")]
        public string StateCode { get; set; }
    }
}

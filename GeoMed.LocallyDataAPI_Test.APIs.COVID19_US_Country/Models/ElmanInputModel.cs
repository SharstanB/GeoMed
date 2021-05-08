using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country.Models
{
    public class ElmanInputModel : USInfoModel
    {
        [Name("cases")]
        public int Cases { get; set; }

    }
}

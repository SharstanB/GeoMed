using GeoMed.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoMed.Model.DataSet
{
    public class CovidZone : BaseModel
    {
     
        public string FipsCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public DateTime Date { get; set; }
        public double Cases { get; set; }
        public string StateCode { get; set; }
        public int Deaths { get; set; }
    }
}

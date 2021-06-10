using GeoMed.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoMed.Model.DataSet
{
    public class SpatialInfo : BaseModel
    {
        public string Country { get; set; }
        public string State { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public int Population { get; set; }
        public double MedianAge { get; set; }
        public ICollection<CovidZone> CovidZones { get; set; }
    }
}

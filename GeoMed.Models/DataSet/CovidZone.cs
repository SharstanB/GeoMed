using GeoMed.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoMed.Model.DataSet
{
    public class CovidZone : BaseModel
    {
        public string FipsCode { get; set; }
        public DateTime Date { get; set; }
        public double Cases { get; set; }
        public string StateCode { get; set; }
        public int Deaths { get; set; }
        public int SpatialInfoId { get; set; }
        public SpatialInfo SpatialInfo { get; set; }
    }
}

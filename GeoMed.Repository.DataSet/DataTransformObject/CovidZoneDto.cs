using GeoMed.Model.DataSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Repository.DataSet.DataTransformObject
{
    public class CovidZoneDto
    {
        public int Id { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string FipsCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public DateTime Date { get; set; }
        public double Cases { get; set; }
        public string StateCode { get; set; }
        public int Deaths { get; set; }

        public static explicit operator CovidZoneDto(CovidZone item)
        {
            return new CovidZoneDto()
            {
                Cases = item.Cases,
                Country = item.Country,
                Date = item.Date,
                Deaths = item.Deaths,
                FipsCode = item.FipsCode,
                Lat = item.Lat,
                Long = item.Long,
                State = item.State,
                StateCode = item.StateCode,
            };
        }
    }
}

using GeoMed.Model.Base;
using GeoMed.Model.Setting;
using GeoMed.SharedKernal.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Model.Main
{
    public class HealthCenter : BaseModel
    {
        public HealthCenter()
        {
            Reviews = new HashSet<Review>();
        }
        public string Name { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
        //doctors
        public decimal? Lat { get; set; }
        public decimal? Log { get; set; }
        public HealthCenters Type { get; set; }
        public TimeSpan? OpeningTime { get; set; }
        public TimeSpan? ClosingTime { get; set; }

        public bool IsAlwaysOpen  =>  OpeningTime is null;

        public ICollection<Review> Reviews { get; set; }

    }
}

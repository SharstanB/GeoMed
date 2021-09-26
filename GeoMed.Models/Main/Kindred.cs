using GeoMed.Model.Base;
using GeoMed.SharedKernal.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Model.Main
{
    public class Kindred : BaseModel
    {
        public KindredLevels Level { get; set; }

        public int? PatientLeftId { get; set; }
        public Patient PatientLeft { get; set; }
        public int? PatientRightId { get; set; }
        public Patient PatientRight { get; set; }
    }
}

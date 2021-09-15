using GeoMed.Model.Base;
using GeoMed.Model.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Model.Setting
{
    /// <summary>
    /// المنطقة
    /// </summary>
    public class Area: BaseModel
    {
        public Area()
        {
            Patients = new HashSet<Patient>();
            HealthCenters = new HashSet<HealthCenter>();
        }
        [Column(TypeName ="nvarchar(50)")]
        public  string Name { get; set; }


        public ICollection<Patient> Patients { get; set; }
        public ICollection<HealthCenter> HealthCenters { get; set; }


    }
}

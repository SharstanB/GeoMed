using GeoMed.Model.Base;
using GeoMed.Model.Main;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoMed.Model.Setting
{
    public class Career :  BaseModel
    {

        public Career()
        {
            Patients = new HashSet<Patient>();
        }
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        public ICollection<Patient>  Patients { get; set; }
    }
}

using GeoMed.Model.Base;
using GeoMed.Model.Templete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Model.Main
{
    public class TrackRecord : BaseModel
    {                       

        [Column(TypeName = "datetime2")]
        public DateTime PreviewDate { get; set; }


        [ForeignKey(nameof(PatientRecordId))]
        public PatientRecord PatientRecord { get; set; }

        [Column(TypeName = "int" )]
        public int PatientRecordId { get; set; }

        public ICollection<Field> Fields { get; set; }
      

    }
}

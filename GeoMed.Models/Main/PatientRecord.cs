using GeoMed.Model.Base;
using GeoMed.Model.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Model.Main
{
    public class PatientRecord  : BaseModel
    {

        public PatientRecord()
        {
            TrackRecords = new HashSet<TrackRecord>();
        }
        [Column(TypeName = "datetime2")]
        public DateTime InComingDate { get; set; }   // تاريخ الدخول

        [Column(TypeName = "datetime2")]
        public DateTime OutComingDate { get; set; } // تاريخ الخروج

        [ForeignKey(nameof(DiseaseID))]
        public Disease Disease { get; set; }


        [Column(TypeName = "int")]
        public int DiseaseID { get; set; }


        [ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; }

        [Column(TypeName = "int")]
        public int PatientId { get; set; }

        public ICollection<TrackRecord> TrackRecords { get; set; }



    }
}

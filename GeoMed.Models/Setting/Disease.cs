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
    public class Disease : BaseModel
    {

        public Disease()
        {
            Symptoms = new HashSet<Symptom>();
            DoctorReviewDiseases = new HashSet<DoctorReviewDisease>();
            Notifications = new HashSet<Notification>();
        }
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        public ICollection<Symptom> Symptoms { get; set; }
        public ICollection<DoctorReviewDisease> DoctorReviewDiseases { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}

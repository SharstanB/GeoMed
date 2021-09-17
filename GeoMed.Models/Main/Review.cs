using GeoMed.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Model.Main
{
    public class Review : BaseModel
    {
        public Review()
        {
            DoctorReviews = new HashSet<DoctorReview>();
        }
        public DateTime Date { get; set; }
        public DateTime NextReviewDate { get; set; }
        public string Note { get; set; }

        public int HealthCenterId { get; set; }
        public HealthCenter HealthCenter { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public ICollection<DoctorReview> DoctorReviews { get; set; }
    }
}

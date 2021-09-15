using GeoMed.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Model.Main
{
    public class DoctorReview : BaseModel
    {
        public DoctorReview()
        {
            DoctorReviewDiseases = new HashSet<DoctorReviewDisease>();
        }
        public string Description { get; set; }
        public string Recipe { get; set; }

        public int ReviewId { get; set; }
        public Review Review { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public ICollection<DoctorReviewDisease> DoctorReviewDiseases { get; set; }
    }
}

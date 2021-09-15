using GeoMed.Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Model.Main
{
    public class DoctorReviewDisease
    {
        public int DoctorReviewId { get; set; }
        public DoctorReview DoctorReview { get; set; }

        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }
    }
}

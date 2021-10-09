using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.MobileService.Dto
{
    public class ReviewDto
    {
        public DateTime Date { get; set; }
        public DateTime NextReviewDate { get; set; }
        public string Note { get; set; }
        public string HealthCenterName { get; set; }

        public string Description { get; set; }
        public string Recipe { get; set; }
        public string DoctorCareer { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.MobileService.Dto
{
    public class InfoReviewsDto : PatientDto
    {
        public IEnumerable<NominalDto> Doctors { get; set; }
        public IEnumerable<NominalDto> HealthCenters { get; set; }
        public IEnumerable<ReviewDto> Reviews { get; set; }
    }
}

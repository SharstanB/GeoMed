using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.MobileService.Dto
{
    public class HomeDto : PatientDto
    {
        public IEnumerable<ReviewDto> Reviews { get; set; }
    }
}

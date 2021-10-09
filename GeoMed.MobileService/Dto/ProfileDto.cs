using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.MobileService.Dto
{
    public class ProfileDto : PatientDto
    {
        public IEnumerable<KindredDto> Kindreds { get; set; }
    }

    public class KindredDto
    {
        public string Level { get; set; }
        public string Name { get; set; }
    }
}

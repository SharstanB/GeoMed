using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.MobileService.Dto
{
    public class DoctorsDto : PatientDto
    {
        public List<ChatDoctorDto> Doctors { get; set; }
    }

    public class ChatDoctorDto: NominalDto
    {
        public List<ChatDto> Chats { get; set; }
    }

    public class ChatDto
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public bool Me { get; set; }
    }
}

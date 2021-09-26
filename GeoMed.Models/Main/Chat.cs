using GeoMed.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Model.Main
{
    public class Chat : BaseModel
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public bool HasSeen { get; set; }

        public int? PatientId { get; set; }
        public Patient Patient { get; set; }
        public int? DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}

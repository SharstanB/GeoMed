using System;
using System.Collections.Generic;
using System.Text;

namespace GeoMed.Main.DTO.Patients
{
   public class GetPatientDto
    {
        public int Id { get; set; } 

        public string PatientName { get; set; }

        public DateTime LastInComeDate { get; set; } 

        public string Address { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }
        public string Career { get; set; }

    }
}

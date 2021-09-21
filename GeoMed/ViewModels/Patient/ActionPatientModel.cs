using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoMed.ViewModels.Patient
{
    public class ActionPatientModel
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime BirthDay { get; set; }

        public int CityId { get; set; }

        public bool Gender { get;set; } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Main.DTO.Patients
{
    public class ActionPatientDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Gender { get; set; }  // enum (Gender)

        public int UserType { get; set; }  // enum(UserTypes)

        public DateTime BirthDate { get; set; }

        public int AreaId { get; set; }

    }
}

using GeoMed.Model.Base;
using GeoMed.Model.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GeoMed.Model.Main
{
    public class Patient : BaseModel
    {
        public Patient()
        {
            PatientRecords = new HashSet<PatientRecord>();
        }

        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        [Column(TypeName = "int")]
        public int Gender { get; set; }  // enum (Gender)

        [Column(TypeName = "int")]
        public int UserType { get; set; }  // enum(UserTypes)

        [Column(TypeName = "datetime2")]
        public DateTime BirthDate { get; set; }

        [ForeignKey(nameof(AreaId))]
        public Area Area { get; set; }

        [Column(TypeName = "int")]
        public int AreaId { get; set; }

        public ICollection<PatientRecord> PatientRecords { get; set; }
    }
}

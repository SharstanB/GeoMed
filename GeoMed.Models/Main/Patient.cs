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
            Chats = new HashSet<Chat>();
            KindredLefts = new HashSet<Kindred>();
            KindredRights = new HashSet<Kindred>();
            Reviews = new HashSet<Review>();
            Patients = new HashSet<Patient>();
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
        public DateTime Birthdate { get; set; }

        [ForeignKey(nameof(AreaId))]
        public Area Area { get; set; }

        [Column(TypeName = "int")]
        public int AreaId { get; set; }

        [ForeignKey(nameof(CareerId))]
        public Career Career { get; set; }


        [Column(TypeName = "int")]
        public int CareerId { get; set; }

        public ICollection<PatientRecord> PatientRecords { get; set; }
        public ICollection<Chat> Chats { get; set; }

        [InverseProperty(nameof(Kindred.PatientLeft))]
        public ICollection<Kindred> KindredLefts { get; set; }

        [InverseProperty(nameof(Kindred.PatientRight))]
        public ICollection<Kindred> KindredRights { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<Patient> Patients { get; set; }
    }
}

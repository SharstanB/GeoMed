using GeoMed.Model.Base;
using GeoMed.Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Model.Main
{
    public class Doctor : BaseModel
    {
        public Doctor()
        {
            Chats = new HashSet<Chat>();
            DoctorReviews = new HashSet<DoctorReview>();
        }
        public string Name { get; set; }
        public int CareerId { get; set; }
        public Career Career { get; set; }

        public ICollection<Chat> Chats { get; set; }
        public ICollection<DoctorReview> DoctorReviews { get; set; }
    }
}

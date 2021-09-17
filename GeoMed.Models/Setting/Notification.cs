using GeoMed.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Model.Setting
{
    public class Notification : BaseModel
    {
        public string Title { get; set; }
        public string Text { get; set; }

        public int? DiseaseId { get; set; }
        public Disease Disease { get; set; }
    }
}

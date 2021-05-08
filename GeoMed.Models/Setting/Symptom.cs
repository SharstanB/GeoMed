using GeoMed.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Model.Setting
{
    public class Symptom : BaseModel
    {
        public Symptom()
        {
            Diseases = new HashSet<Disease>();
        }

        [Column(TypeName ="nvarchar(50)")]
        public string Name { get; set; }
        public ICollection<Disease> Diseases { get; set; }
    }
}

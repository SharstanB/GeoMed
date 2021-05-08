using GeoMed.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Model.Templete
{
    public class Templete : BaseModel
    {
        public string Path { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<Field> Fields { get; set; }
             
    }
}

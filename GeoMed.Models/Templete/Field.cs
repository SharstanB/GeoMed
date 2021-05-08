using GeoMed.Model.Base;
using GeoMed.Model.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Model.Templete
{
    public class Field : BaseModel
    {
        [Column(TypeName = "nvarchar(100)")]
        public  string Value { get; set; }

        [Column(TypeName ="int")]
        public  int FieldType { get; set; }


        [ForeignKey(nameof(TempleteId))]

        public Templete Templete { get; set; }


        [Column(TypeName = "int")]
        public int TempleteId { get; set; }

        [ForeignKey(nameof(TrackRecordId))]

        public TrackRecord TrackRecord { get; set; }


        [Column(TypeName = "int")]
        public int TrackRecordId { get; set; }
    }
}

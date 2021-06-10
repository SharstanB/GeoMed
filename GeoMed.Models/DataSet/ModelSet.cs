using GeoMed.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GeoMed.Model.DataSet
{
    public class ModelSet : BaseModel
    {
        public double ErrorRate { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Path { get; set; }

        [Column(TypeName = "int")]
        public int AlgorithmType { get; set; }

    }
}

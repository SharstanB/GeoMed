using GeoMed.NN.Base.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Repository.DataSet.DataTransformObject
{
    public class MapDataDto
    {
        public ExecutedData ExecutedData { get; set; } = ExecutedData.County;

        public DateTime DateTime { get; set; }
    }
}

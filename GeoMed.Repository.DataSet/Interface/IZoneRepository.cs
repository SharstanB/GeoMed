using GeoMed.Base;
using GeoMed.Repository.DataSet.DataTransformObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Repository.DataSet.Interface
{
    public interface IZoneRepository
    {
        public Task<OperationResult<IEnumerable<CovidZoneDto>>> USAAggregate();
    }
}

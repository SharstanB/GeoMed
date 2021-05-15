using GeoMed.Base;
using GeoMed.Main.DTO.Patients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoMed.Main.IData.IRepositories
{
    public interface IPatientRepository
    {

        public Task<OperationResult<IEnumerable<GetPatientDto>>> GetPatientsData();
    }
}

using GeoMed.Base;
using GeoMed.Main.DTO.Patients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoMed.Main.IData.IRepositories
{
    public interface IPatientRepository 
    {

        public OperationResult<IEnumerable<GetPatientDto>> GetPatientsData();

        public  OperationResult<GetPatientDto> ActionPatient(ActionPatientDto actionPatient);
    }
}

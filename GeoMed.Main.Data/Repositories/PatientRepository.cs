using GeoMed.Base;
using GeoMed.Main.DTO.Patients;
using GeoMed.Main.IData.IRepositories;
using GeoMed.SharedKernal.Enums;
using GeoMed.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoMed.Main.Data.Repositories
{
    public class PatientRepository : BaseRepository  , IPatientRepository
    {
        public PatientRepository (GMContext context):
            base(context)
        {

        }

        public async Task<OperationResult<IEnumerable<GetPatientDto>>> GetPatientsData()
        {
            OperationResult<IEnumerable<GetPatientDto>> operationResult = new OperationResult<IEnumerable<GetPatientDto>>();

            try
            {
                operationResult.Result = await Context.Patients.Select(patient => new GetPatientDto()
                {
                    Address = patient.Area.Name,
                    Age =  EF.Functions.DateDiffYear(patient.Birthdate, DateTime.Now),
                    Gender = Enum.GetName(typeof(Gender), patient.Gender),
                    Id = patient.Id,
                     LastInComeDate = patient.PatientRecords.OrderBy(order => order.InComingDate)
                     .LastOrDefault().InComingDate,
                    PatientName = patient.FirstName ?? "" + " " + patient.LastName ?? "" ,
                    
                }).ToListAsync();

                operationResult.OperationResultType = OperationResultTypes.Success;
               
            }
            catch (Exception ex)
            {
                operationResult.OperationResultType = OperationResultTypes.Exeption;
                operationResult.Exception = ex;
            }

            return operationResult;
        }
    }
}

using GeoMed.Base;
using GeoMed.Main.DTO.Patients;
using GeoMed.Main.IData.IRepositories;
using GeoMed.SharedKernal.Enums;
using GeoMed.SqlServer;
using GM.QueueService.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoMed.Main.Data.Repositories
{
    public class PatientRepository : BaseRepository  , IPatientRepository
    {

        public IQueueService QueueService { get; }
        public PatientRepository (GMContext context , IQueueService queueService):
            base(context)
        {
            QueueService = queueService;
        }

        public async Task<OperationResult<bool>> ActionPatient(ActionPatientDto actionPatient)
        {
            var result = new OperationResult<bool>();

            //Context.Patients.Add(new Model.Main.Patient()
            //{
            //    AreaId = actionPatient.AreaId,
            //    Birthdate = actionPatient.BirthDate,
            //    FirstName = actionPatient.FirstName,
            //    Gender = actionPatient.Gender,
            //    LastName = actionPatient.LastName,
            //    UserType = (int)UserType.patient,
            //});

             QueueService.Publish(new GetPatientDto()
             {
                 Address = "sd",
                 Gender = Gender.famale.ToString(),
                 Age = 23,
                 LastInComeDate = DateTime.Now,
                 PatientName = "sddmf lsldkkmf ",
             });

            return result;
        }

        public async Task<OperationResult<IEnumerable<GetPatientDto>>> GetPatientsData()
        {
            OperationResult<IEnumerable<GetPatientDto>> operationResult = new OperationResult<IEnumerable<GetPatientDto>>();

            try
            {
                //operationResult.Result = await Context.Patients.Select(patient => new GetPatientDto()
                //{
                //    Address = patient.Area.Name,
                //    Age =  EF.Functions.DateDiffYear(patient.Birthdate, DateTime.Now),
                //    Gender = Enum.GetName(typeof(Gender), patient.Gender),
                //    Id = patient.Id,
                //     LastInComeDate = patient.PatientRecords.OrderBy(order => order.InComingDate)
                //     .LastOrDefault().InComingDate,
                //    PatientName = patient.FirstName ?? "" + " " + patient.LastName ?? "" ,
                    
                //}).ToListAsync();

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

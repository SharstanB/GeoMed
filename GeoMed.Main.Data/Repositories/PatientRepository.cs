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

        public OperationResult<GetPatientDto> ActionPatient(ActionPatientDto actionPatient)
        {


            var newPatient = new Model.Main.Patient()
            {
                AreaId = actionPatient.AreaId,
                Birthdate = actionPatient.BirthDate.Value,
                FirstName = actionPatient.FirstName,
                Gender = actionPatient.Gender,
                LastName = actionPatient.LastName,
                UserType = (int)UserType.patient,
            };
           
           Context.Patients.Add(newPatient);

            Context.SaveChanges();
            //QueueService.Publish(new GetPatientDto()
            // {
            //     Address = "sd",
            //     Gender = Gender.famale.ToString(),
            //     Age = 23,
            //     LastInComeDate = DateTime.Now,
            //     PatientName = "sddmf lsldkkmf ",
            // });

            return new OperationResult<GetPatientDto>()
            {
                Result = new GetPatientDto()
                {
                    Address = Context.Areas
                    .SingleOrDefault(area => area.Id == actionPatient.AreaId).Name,
                    Age = Convert.ToDateTime(DateTime.Now - actionPatient.BirthDate).Year,
                    Career = Context.Careers.SingleOrDefault(career => 
                    career.Id == actionPatient.CareerId).Name,
                    Id = newPatient.Id,
                    Gender = Enum.GetName(typeof(Gender) , actionPatient.Gender),
                    LastInComeDate = newPatient.CreateDate,
                    PatientName = $"{newPatient.FirstName} {newPatient.LastName}"
                }
            };
        }

        public async Task<OperationResult<IEnumerable<GetPatientDto>>> GetPatientsData()
        {
            OperationResult<IEnumerable<GetPatientDto>> operationResult = new OperationResult<IEnumerable<GetPatientDto>>();

            try
            {
                operationResult.Result = await Context.Patients.Select(patient => new GetPatientDto()
                {
                    Address = patient.Area.Name,
                    Age = EF.Functions.DateDiffYear(patient.Birthdate, DateTime.Now),
                    Gender = Enum.GetName(typeof(Gender), patient.Gender),
                    Id = patient.Id,
                    LastInComeDate = patient.PatientRecords.OrderBy(order => order.InComingDate)
                     .LastOrDefault().InComingDate,
                    PatientName = patient.FirstName ?? "" + " " + patient.LastName ?? "",

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

using GeoMed.Base;
using GeoMed.Main.DTO.Patients;
using GeoMed.Main.IData.IRepositories;
using GeoMed.SharedKernal.Enums;
using GeoMed.SharedKernal.Util;
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
                CareerId = actionPatient.CareerId ,
                BloodType = actionPatient.BloodType,
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
                    Age = actionPatient.BirthDate.Value.ToformatingDate(),
                    Career = Context.Careers.SingleOrDefault(career => 
                    career.Id == actionPatient.CareerId).Name,
                    Id = newPatient.Id,
                    Gender = Enum.GetName(typeof(Gender) , actionPatient.Gender),
                    LastInComeDate = newPatient.CreateDate,
                    PatientName = $"{newPatient.FirstName} {newPatient.LastName}",
                    BloodType = Enum.GetName(typeof(BloodType), actionPatient.BloodType),
                }
            };
        }

        public OperationResult<IEnumerable<GetPatientDto>> GetPatientsData()
        {
            OperationResult<IEnumerable<GetPatientDto>> operationResult = new OperationResult<IEnumerable<GetPatientDto>>();

            try
            {
                operationResult.Result = Context.Patients
                    .Include(patient => patient.PatientRecords)
                    .Include(patient => patient.Career)
                    .Include(patient => patient.Area)
                    .ToList().Select(patient => new GetPatientDto()
                {
                    Address = patient.Area.Name,
                    Age = patient.Birthdate.ToformatingDate(),
                    Gender = Enum.GetName(typeof(Gender), patient.Gender),
                    Id = patient.Id,
                    LastInComeDate = patient.PatientRecords.OrderBy(order => order.InComingDate)
                     .LastOrDefault()?.InComingDate ?? DateTime.Now,
                    PatientName = patient.FirstName ?? "" + " " + patient.LastName ?? "",
                    BloodType = Enum.GetName(typeof(Gender), patient.BloodType),
                    Career = patient.Career.Name
                    });

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

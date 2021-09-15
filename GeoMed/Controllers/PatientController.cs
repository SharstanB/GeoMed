using GeoMed.Main.DTO.Patients;
using GeoMed.Main.IData.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace GeoMed.Controllers
{
    public class PatientController : Controller
    {
        private IPatientRepository PatientRepository { get; }

        public PatientController(IPatientRepository patientRepository)
        {
            PatientRepository = patientRepository;
        }
        public IActionResult Index()
        {             


            return View();
        }

        public IActionResult ActionPatient()
        {
            PatientRepository.ActionPatient(new ActionPatientDto()
            {
                AreaId = 2,
                BirthDate = System.DateTime.Now,
                FirstName = "aAA",
                Gender = 3,
                LastName = "ASD",
            });

            return View();
        }

    }
}

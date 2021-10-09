using GeoMed.ViewModels.Patient;
using System.Threading.Tasks;
using GeoMed.Main.IData.IRepositories;
using System.Collections.Generic;
using GeoMed.Main.DTO.Patients;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using GeoMed.Views.Patient.Components.Modal;
using System.Linq;

namespace GeoMed.Views.Patient.Components.Table
{
    public partial class PatientTable : ComponentBase
    {
        private PatientSearchViewModel PatientSearch =
           new PatientSearchViewModel();

        [Inject]
        public IPatientRepository PatientRepository { get; set; }

        private PatientModal child { get; set; }

        public int Count { get; set; }

        public int PageSize { get; set; }

        List<GetPatientDto> Patients = new List<GetPatientDto>();
        protected override void OnInitialized()
        {
            Patients =  PatientRepository.GetPatientsData()
                .Result.ToList() ?? new List<GetPatientDto>();
        }
        public void OnSearch()
        {

        }

        public async Task AddToTable(GetPatientDto patientDto)
        {
            Patients.Add(patientDto);
            await CloseModal();
        }

        public  void OnSelectPage()
        {

        }

        public async Task CloseModal()
        {
            await child.CloseModal();
        }
        public async Task OpenModal()
        {
            await child.OpenModal();
        }
    }
}

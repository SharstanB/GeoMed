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

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

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

        public void AddToTable(GetPatientDto patientDto)
        {
            Patients.Add(patientDto);
        }
        public  void OnSelectPage()
        {

        }
      

        public async Task OpenModal()
        {
            await child.OpenModal();
        }
    }
}

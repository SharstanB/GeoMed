using GeoMed.ViewModels.Patient;
using System.Threading.Tasks;
using GeoMed.Main.IData.IRepositories;
using System.Collections.Generic;
using GeoMed.Main.DTO.Patients;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using GeoMed.Views.Patient.Components.Modal;

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

        IEnumerable<GetPatientDto> Patients { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Patients = (await PatientRepository.GetPatientsData())
                .Result ?? new List<GetPatientDto>();
        }
        public void OnSearch()
        {

        }

        public async Task OnSelectPage()
        {

        }
      

        public async Task OpenModal()
        {
            await child.OpenModal();
        }
    }
}

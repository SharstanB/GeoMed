using GeoMed.ViewModels.Patient;
using System.Threading.Tasks;
using GeoMed.Main.IData.IRepositories;
using System.Collections.Generic;
using GeoMed.Main.DTO.Patients;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace GeoMed.Views.Patient.Components.Table
{
    public partial class PatientTable
    {
        private PatientSearchViewModel PatientSearch =
           new PatientSearchViewModel();

        [Inject]
        public IPatientRepository PatientRepository { get; set; }

        public ElementReference ModalPatient { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }


        public int Count { get; set; }

        public int PageSize { get; set; }

        IEnumerable<GetPatientDto> Patients { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Patients = (await PatientRepository.GetPatientsData())
                .IEnumerableResult ?? new List<GetPatientDto>();
        }
        public void OnSearch()
        {

        }

        public async Task OnSelectPage()
        {

        }
        public async Task OpenModal()
        {
             await JSRuntime.InvokeVoidAsync("blazorShowModal", ModalPatient);
        }
    }
}

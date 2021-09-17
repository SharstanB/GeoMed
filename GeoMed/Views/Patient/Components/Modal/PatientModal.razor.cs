using GeoMed.Main.DTO.Patients;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace GeoMed.Views.Patient.Components.Modal
{
    public partial class PatientModal
    {
        [Parameter]
        public ElementReference ModalPatient { get; set; }
        [Parameter]
        public EventCallback<bool> OnOpen { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }


        public async Task OpenModal()
        {
            await JSRuntime.InvokeVoidAsync("showModal", ModalPatient);
        }

    }
}

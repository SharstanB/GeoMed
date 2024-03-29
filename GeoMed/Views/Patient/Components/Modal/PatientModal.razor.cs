﻿using GeoMed.Main.DTO.Patients;
using GeoMed.Main.IData.IRepositories;
using GeoMed.Views.Patient.Components.Table;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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

        public ActionPatientDto ActionPatient { get;set; }

        private PatientTable parent = new PatientTable();

        [Inject]
        public IPatientRepository PatientRepository { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            ActionPatient = new ActionPatientDto();

        }

        public async Task Save(EditContext editContext)
        {
          var newPatient =
                PatientRepository.ActionPatient(ActionPatient);
            if(newPatient.Issuccess)
            await parent.AddToTable(newPatient.Result);
        }
        public async Task OpenModal()
        {
            await JSRuntime.InvokeVoidAsync("showModal", ModalPatient);
        }
        public async Task CloseModal()
        {
            await JSRuntime.InvokeVoidAsync("closeModal", ModalPatient);
        }

    }
}

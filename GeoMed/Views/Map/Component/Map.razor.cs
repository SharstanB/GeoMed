using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GeoMed.Views.Map.Component
{
    public partial class Map : ComponentBase
    {

        [Inject]
        public IJSRuntime JS { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JS.InvokeVoidAsync("initializeMap", null);

                StateHasChanged();
            }

        }
    }
}

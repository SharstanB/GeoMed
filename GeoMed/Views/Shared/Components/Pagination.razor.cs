using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoMed.Views.Shared.Components
{
    public partial class Pagination
    {
       
        [Parameter]
        public int PageSize { get; set; }
       
        [Parameter]
        public int Spread { get; set; }

        [Parameter]
        public EventCallback<int> SelectedPage { get; set; }

        [Parameter]
        public int TotalCount { get; set; }


        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        

        private List<PagingLink> _links;

        protected override void OnInitialized()
        {
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
        }
        protected override void OnParametersSet()
        {
            CreatePaginationLinks();
        }
        private void CreatePaginationLinks()
        {

            _links = new List<PagingLink>();
            _links.Add(new PagingLink(CurrentPage - 1, HasPrevious, ">"));
            for (int i = 1; i <= TotalPages; i++)
            {
                if (i >= CurrentPage - Spread && i <= CurrentPage + Spread)
                {
                    _links.Add(new PagingLink(i, true, i.ToString()) { Active = CurrentPage == i });
                }
            }
            _links.Add(new PagingLink(CurrentPage + 1, HasNext, "<"));
        }
        private async Task OnSelectedPage(PagingLink link)
        {
            if (!link.Enabled) return;
            CurrentPage = link.Page;
            await SelectedPage.InvokeAsync();
        }
    }




    public class PagingLink
    {
        public string Text { get; set; }
        public int Page { get; set; }
        public bool Enabled { get; set; }
        public bool Active { get; set; }
        public PagingLink(int page, bool enabled, string text)
        {
            Page = page;
            Enabled = enabled;
            Text = text;
        }
    }

    //public class MetaData
    //{
    //    public int CurrentPage { get; set; }
    //    public int TotalPages { get; set; }
    //    public int PageSize { get; set; }
    //    public int TotalCount { get; set; }
    //    public bool HasPrevious => CurrentPage > 1;
    //    public bool HasNext => CurrentPage < TotalPages;
    //}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoMed.Util
{
    public static class ViewPaths
    {
        public static string BlazorImports = "~/Views/_BlazorImports.razor";

        public static string Scripts = "~/Views/Shared/_Scripts.cshtml";

        public static string HeaderNavbar = "~/Views/Shared/_HeaderNavbar.cshtml";

        public static string Links = "~/Views/Shared/_Links.cshtml";

        public static string LoginLayout = "~/Views/Shared/_LoginLayout.cshtml";

        /// <summary>
        /// Views/Shared/_Meta.cshtml
        /// </summary>
        public static string _Meta { get; } = "~/Views/Shared/_Meta.cshtml";

        /// <summary>
        /// Views/Shared/Partials/Navabar.cshtml
        /// </summary>
        public static string Navbar { get; } = "~/Views/Shared/Partials/Navbar.cshtml";

        /// <summary>
        /// Views/Shared/Partials/Footer.cshtml
        /// </summary>
        public static string Footer { get; } = "~/Views/Shared/Partials/Footer.cshtml";

        /// <summary>
        /// Views/Shared/Partials/Sidebar.cshtml
        /// </summary>
        public static string Sidebar { get; } = "~/Views/Shared/Partials/Sidebar.cshtml";

        /// <summary>
        /// Views/Shared/Partials/Breadcrumb.cshtml
        /// </summary>
        public static string Breadcrumb { get; } = "~/Views/Shared/Partials/Breadcrumb.cshtml";

        /// <summary>
        /// Views/Shared/Partials/Drawer.cshtml
        /// </summary>
        public static string Drawer { get; } = "~/Views/Shared/Partials/Drawer.cshtml";

        /// <summary>
        /// Views/Shared/Partials/StickyTool.cshtml
        /// </summary>
        public static string StickyTool { get; } = "~/Views/Shared/Partials/StickyTool.cshtml";

        public static string QuikNav { get; } = "~/Views/Shared/Partials/QuikNav.cshtml";
    }
}

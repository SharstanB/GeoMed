using GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GeoMed.Controllers
{
    [Authorize(AuthenticationSchemes = "Cookies")]
    public class HomeController : Controller
    {
       // private readonly ILogger<HomeController> _logger;

        public HomeController()
        {
          //  _logger = logger;
        }

        public IActionResult Index()
        {
            //var equation = COVID19USCountry.GetDataInput(NN.Base.Enums.ExecutedData.County);

            ViewData["Title"] = "الرئيسية";
            return View();
        }

       
    }
}

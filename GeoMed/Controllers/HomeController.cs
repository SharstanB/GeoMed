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
            ViewData["Title"] = "الرئيسية";
            return View();
        }

       
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoMed.Controllers
{
    public class TempleteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

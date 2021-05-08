using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoMed.Controllers
{
    [Authorize]
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GoogleMap()
        {
            return View();
        }

        public IActionResult USA()
        {
            return View("VectorMap");
        }

        [HttpGet]
        public IActionResult USA_Covid([FromQuery]string state)
        {
            return Ok(LocallyDataAPI_Test.APIs.COVID19_US_Country.COVID19USCountry.USAAggregate());
        }
    }
}

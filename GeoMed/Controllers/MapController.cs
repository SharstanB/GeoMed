using GeoMed.Repository.DataSet.Interface;
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
        private readonly IZoneRepository zone;

        public MapController(IZoneRepository zone)
        {
            this.zone = zone;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GoogleMap()
        {
            return View();
        }

        public async Task<IActionResult> USA()
        {
            var das = (await zone.USAAggregate()).Result;
            return View("VectorMap");
        }

        [HttpGet]
        public async Task<IActionResult> USA_CovidAsync([FromQuery]string state)
        {
            return Json( (await zone.USAAggregate()).Result);
            return Ok(LocallyDataAPI_Test.APIs.COVID19_US_Country.COVID19USCountry.USAAggregate());
        }
    }
}

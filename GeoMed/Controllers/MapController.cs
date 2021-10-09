using GeoMed.Repository.DataSet.DataTransformObject;
using GeoMed.Repository.DataSet.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GeoMed.Controllers
{
    [Authorize]
    public class MapController : Controller
    {
        private readonly IWebHostEnvironment webHost;
        private readonly IZoneRepository zone;

        public MapController(IWebHostEnvironment webHost , IZoneRepository zone)
        {
            this.webHost = webHost;
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

        [AllowAnonymous]
        public IActionResult USA()
        {
            return View("VectorMap");
        }

        //public async Task<IActionResult> USA()
        //{
        //    var das = (await zone.USAAggregate()).Result;
        //    return View("VectorMap");
        //}

        [HttpGet]
        public async Task<IActionResult> USA_Covid([FromQuery]string state)
        {
            var fullpath = Path.Combine(webHost.WebRootPath, "results", "CovidZoneDtoResult.json");
            var res = System.IO.File.ReadAllText(fullpath);
           // var data = System.Text.Json.JsonSerializer.Deserialize<CovidZoneDto>(res);
            return Json(res);

            return Json( (await zone.USAAggregate()).Result);
            return Ok(LocallyDataAPI_Test.APIs.COVID19_US_Country.COVID19USCountry.USAAggregate());
        }

        [HttpGet]
        public async Task<IActionResult> USA_CovidOneDay()
        {
            var fullpath = Path.Combine(webHost.WebRootPath, "results", "CovidZoneOneDtoResult.json");
            var res = System.IO.File.ReadAllText(fullpath);
            return Json(res);

           // return Json((await zone.USAOne()).Result);
        }

        [HttpGet]
        public async Task<IActionResult> USA_CovidTenDay()
        {
            var fullpath = Path.Combine(webHost.WebRootPath, "results", "CovidZoneTenDtoResult.json");
            var res = System.IO.File.ReadAllText(fullpath);
            return Json(res);

            //return Json((await zone.USATen()).Result);
        }
    }
}

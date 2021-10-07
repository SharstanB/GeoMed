using GeoMed.MobileService.IData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoMed.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MobileController : ControllerBase
    {
        private readonly IMobileRepository repository;

        public MobileController(IMobileRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Test()
        {
           return new JsonResult(repository.TestMessage()) {StatusCode=200};
        }
    }
}

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

        [HttpPost]
        public async Task<IActionResult> Login(GeoMed.MobileService.Dto.LoginDto login)
        {
            var res =await repository.Login(login);
            return new JsonResult(res.Result) { StatusCode = (res.Result.Id == 0)?403:200 };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Home([FromRoute]int id)
        {
            var res = await repository.Home(id);
            return new JsonResult(res.Result) { StatusCode = 200 };
        }

        [HttpPost]
        public async Task<IActionResult> Register(GeoMed.MobileService.Dto.RegisterDto reg)
        {
            var res = await repository.Register(reg);
            return new JsonResult(res.Result) { StatusCode =res.Result?200:400 };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Reviews([FromRoute] int id)
        {
            var res = await repository.Reviews(id);
            return new JsonResult(res.Result) { StatusCode = 200 };
        }
        

        [HttpPost("{id}")]
        public async Task<IActionResult> AddReview([FromRoute] int id, GeoMed.MobileService.Dto.ReviewDto review)
        {
            var res = await repository.AddReview(id, review);
            return new JsonResult(res.Result) { StatusCode = res.Result ? 200 : 400 };
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Doctors([FromRoute] int id)
        {
            var res = await repository.Doctors(id);
            return new JsonResult(res.Result) { StatusCode = 200 };
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Profile([FromRoute] int id)
        {
            var res = await repository.Profile(id);
            return new JsonResult(res.Result) { StatusCode = 200 };
        }

    }
}

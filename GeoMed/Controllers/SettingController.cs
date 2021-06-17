using GeoMed.Main.IData.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoMed.Controllers
{
    public class SettingController : Controller
    {

        public  INNRepository NNRepository { get;  }

        public SettingController(INNRepository nNRepository)
        {
            NNRepository = nNRepository;
        }
        public IActionResult Index()
        {
          //  NNRepository.LoadModel("");
            return View();
        }
    }
}

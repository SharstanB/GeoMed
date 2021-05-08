using GeoMed.Share.IData.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoMed.Controllers
{
    public class SharedController : Controller
    {
        private IStoreDataRepository StoreDataRepository { get; }

        public SharedController(IStoreDataRepository storeDataRepository)
        {
            StoreDataRepository = storeDataRepository;
        }
        public IActionResult Index()
        {
            StoreDataRepository.ReadExcelData();
            return View();
        }

        public IActionResult Status(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if ( statusCode.Value == 403 || statusCode.Value == 401)
                {
                    return View("Forbidden403Page");//Forbidden403Page
                }
                if(statusCode.Value == 404) 
                {
                    return View("NotFound");
                }
            }
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace GeoMed.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

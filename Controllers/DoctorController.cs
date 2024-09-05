using Microsoft.AspNetCore.Mvc;

namespace HospitalCase.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

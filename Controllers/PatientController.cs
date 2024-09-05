using Microsoft.AspNetCore.Mvc;

namespace HospitalCase.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

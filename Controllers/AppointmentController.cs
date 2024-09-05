using Microsoft.AspNetCore.Mvc;

namespace HospitalCase.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

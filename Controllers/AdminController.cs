﻿using Microsoft.AspNetCore.Mvc;

namespace HospitalCase.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

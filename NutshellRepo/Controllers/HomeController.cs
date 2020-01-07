using Microsoft.AspNetCore.Mvc;
using System;

namespace NutshellRepo.Controllers
{
    public class HomeController : Controller
    {       
        public IActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Nutshell(string search)
        {
            ViewBag.text1 = search;
            return View("NotImplemented");            
        }

    }
}

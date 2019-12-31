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

    }
}

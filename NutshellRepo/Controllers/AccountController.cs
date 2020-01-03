using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutshellRepo.ViewModels;

namespace NutshellRepo.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]        
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Register(AccountRegisterViewModel aAccountRegisterViewModel)
        {
            //ViewBag.text1 = aAccountRegisterViewModel.UserName;
            //return View("NotImplemented");
            return View();
        }

    }
}

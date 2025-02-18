﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NutshellRepo.Data.Security;
using NutshellRepo.Models;
using NutshellRepo.Utilities.Email.HTMLTemplates;
using NutshellRepo.ViewModels.Account;
using System;
using System.Threading.Tasks;

namespace NutshellRepo.Controllers
{
    public class AccountController : Controller
    {
        #region Private Member Injections
        private readonly UserManager<Member> _MemberManager;
        private readonly SignInManager<Member> _SignInManager;
        private readonly ITemplatedEmailSender _TemplatedEmailSender;
        private readonly IDataProtector _dataProtector;
        #endregion

        #region Constructor
        public AccountController(
                                    UserManager<Member> aMemberManager,
                                    SignInManager<Member> aSignInManager,
                                    ITemplatedEmailSender aTemplatedEmailSender,
                                    DataProtectionPurposeStrings aDataProtectionPurposeStrings,
                                    IDataProtectionProvider aDataProtectionProvider

                                )
        {
            _MemberManager = aMemberManager;
            _SignInManager = aSignInManager;
            _TemplatedEmailSender = aTemplatedEmailSender;
            _dataProtector = aDataProtectionProvider.CreateProtector(aDataProtectionPurposeStrings.UserIdRouteValue);
        }
        #endregion

        #region Username Remote Validation

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsUserNameTaken(string userName)
        {
            var user = await _MemberManager.FindByNameAsync(userName);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"User Name: {userName} is already in use");
            }

        }

        #endregion

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            var user = await _MemberManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return View();
            }
            //already logged in
            return RedirectToAction("index", "home");            
        }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(AccountRegisterViewModel aAccountRegisterViewModel)
        {
            if (ModelState.IsValid)
            {
                Member member;

                try
                {
                    member = await _MemberManager.FindByNameAsync(aAccountRegisterViewModel.UserName);
                }
                catch (Exception ex)
                {
                    //TODO log - cant reach DB
                    var ziu = ex;

                    ViewBag.ExceptionPath = "account/register";
                    ViewBag.ExceptionMessage = "DataBase connection";
                    ViewBag.Stacktrace = "Cannot Reach DataBase";

                    return View("Exception");
                    
                }
                

                if (member != null)
                {
                    ModelState.AddModelError("", $"User Name {aAccountRegisterViewModel.UserName} already taken.");
                    return View();
                }
                else
                {
                    member = new Member
                    {
                        UserName = aAccountRegisterViewModel.UserName,
                        Email = aAccountRegisterViewModel.Email,
                        RegisteredOn = DateTime.Now
                    };
                                                                             
                    var result = await _MemberManager.CreateAsync(member, aAccountRegisterViewModel.Password);

                    if (result.Succeeded)
                    {
                        var token = await _MemberManager.GenerateEmailConfirmationTokenAsync(member);
                        

                        var confirmationLink = Url.Action(
                                 "ConfirmEmail"
                                , "Account"
                                ,new { userId = _dataProtector.Protect(member.Id), token }
                                ,Request.Scheme
                            );

                        var response = await _TemplatedEmailSender
                            .SendConfirmationEmailAsync(
                                    member.Email,
                                    member.UserName,
                                    confirmationLink);
                        

                        if (response.Successful)
                        {
                            return View("ConfirmationSent");
                        }

                        return View("FailToSendEmail", response);

                    }


                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }
                
            }


            return View(aAccountRegisterViewModel);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {

            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            string decUserId;

            try
            {
                decUserId = _dataProtector.Unprotect(userId);
            }
            catch (Exception)
            {
                return View("FailToConfirmEmail", "Wrong Confirmation.");
            }


            var user = await _MemberManager.FindByIdAsync(decUserId);

            if (user == null)
            {
                return View("FailToConfirmEmail", "Cannot find specified user.");
            }

            var isAlreadyConfirmed = await _MemberManager.IsEmailConfirmedAsync(user);

            if (isAlreadyConfirmed)
            {
                return View("AlreadyConfirmed");
            }

            var result = await _MemberManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return View();
            }

            return View("FailToConfirmEmail", result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn()
        {            
            var user = await _MemberManager.GetUserAsync(HttpContext.User);
            if (user==null)
            {
                return View();
            }
            //already logged in
            return RedirectToAction("index", "home");
        }

        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInViewModel logInViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _MemberManager.FindByEmailAsync(logInViewModel.Email);

                if (user!=null)
                {
                    var result = await _SignInManager.PasswordSignInAsync(user, logInViewModel.Password, logInViewModel.RememberMe, false);

                    if (result.Succeeded)
                    {                        
                        return RedirectToAction("index", "home");
                    }
                                            
                }

                ModelState.AddModelError("", "Invalid Login Attempt");                            

            }            

            return View(logInViewModel);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {            
            await _SignInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }



    }
}

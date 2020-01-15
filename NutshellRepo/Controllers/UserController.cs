using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutshellRepo.Data.DB;
using NutshellRepo.Data.Security;
using NutshellRepo.Models;
using NutshellRepo.ViewModels.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutshellRepo.Controllers
{
    public class UserController : Controller
    {
        #region CTOR
        private readonly UserManager<Member> _MemberManager;
        private readonly SignInManager<Member> _SignInManager;
        private readonly StudyLogBookDbContext _DbContext;
        private readonly IDataProtector _dataProtector;

        public UserController(
            UserManager<Member> aMemberManager,
            SignInManager<Member> aSignInManager,
            StudyLogBookDbContext aDbContext,
            DataProtectionPurposeStrings aDataProtectionPurposeStrings,
            IDataProtectionProvider aDataProtectionProvider
            )
        {
            _MemberManager = aMemberManager;
            _SignInManager = aSignInManager;
            _DbContext = aDbContext;
            _dataProtector = aDataProtectionProvider.CreateProtector(aDataProtectionPurposeStrings.MessageIdRouteValue);
        }
        #endregion

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ViewProfile(string UserName)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return RedirectToAction("index", "home");
            }            
            
            var user = await _MemberManager.FindByNameAsync(UserName);

            if (user!=null)
            {
                if (    HttpContext.User.Identity.IsAuthenticated
                     && HttpContext.User.Identity.Name.ToUpper() == user.NormalizedUserName
                    )
                {                    
                    return View("PersonalCabinet",user);
                }

                //return info page
                return View("ViewProfile", user.UserName);
                //return Content(user.RegisteredOn.ToString("dd MMMM yyyy") +" "+user.UserName);

            }
            
            return Content("no such user");

        }

        [HttpPost]
        public async Task<IActionResult> ViewInbox()
        {
            var user = await _MemberManager.GetUserAsync(User);

            if (user!=null)
            {
                var messages = _DbContext.Messages.Where(MSG => MSG.ToUserId == user.Id);
                if(messages!=null)
                {
                    var userPreviewMessages = new List<UserPreviewMessageViewModel>();
                    foreach (var message in messages) {

                        userPreviewMessages.Add(new UserPreviewMessageViewModel() 
                        { 
                            Id = _dataProtector.Protect(message.Id),
                            FromUser = message.FromUser,
                            Subject = message.Subject,
                            MessageBody = message.MessageBody,

                        });

                    }

                    return View("ViewInboxList", userPreviewMessages);

                }

            }

            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public async Task<IActionResult> ReadMessage(string messageId)
        {

            //return await Task.Run(()=> Content(messageId));
            return await Task.Run(()=> Content(_dataProtector.Unprotect(messageId)));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMessages(List<UserPreviewMessageViewModel> aUserPreviewMessageViewModel)
        {
            var user = await _MemberManager.GetUserAsync(User);

            if (user != null)
            {
                
                if (aUserPreviewMessageViewModel != null && aUserPreviewMessageViewModel.Count() > 0)
                {
                    var messagesToShow = new List<UserPreviewMessageViewModel>();

                    foreach (var item in aUserPreviewMessageViewModel)
                    {
                        if (item.isToDelete == true)
                        {
                            var userMessageToDelete = new UserMessage() { Id = _dataProtector.Unprotect(item.Id) };
                            _DbContext.Remove(userMessageToDelete);
                        }
                        else
                        {
                            messagesToShow.Add(item);
                        }
                    }

                    try
                    {
                        await _DbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException ex)
                    {
                        throw ex;
                    }

                    ModelState.Clear();
                    return View("ViewInboxList", messagesToShow);

                }
                

            }

            return RedirectToAction("index", "home");
        }















        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MessagesCount()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var result = await _MemberManager.GetUserAsync(HttpContext.User);
                var data = new { status = "ok", result = result.UnreadMessages };
                return Json(data);
            }

            return Json("Access Denied");

        }


    }
    

}
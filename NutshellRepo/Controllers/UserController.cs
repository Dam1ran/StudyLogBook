using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutshellRepo.Data.DB;
using NutshellRepo.Data.Security;
using NutshellRepo.Models;
using NutshellRepo.ViewModels.User;
using System;
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
        [ValidateAntiForgeryToken]
        public IActionResult ViewInbox()
        {
            return View();           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ViewInboxList(int PageNumber,string PageNav,string SearchString)
        {
            var user = await _MemberManager.GetUserAsync(User);

            await Task.Delay(1000);

            if (user != null)
            {
                IQueryable<UserMessage> messages = null;
                PageNavigationHelper pageHelper = null;
                int numberOfmessages;

                if (string.IsNullOrEmpty(SearchString))
                {
                    numberOfmessages = _DbContext.Messages.Count(MSG => MSG.ToUserId == user.Id);
                    pageHelper = new PageNavigationHelper(numberOfmessages, 10, PageNumber - 1, PageNav);
                    messages = _DbContext.Messages
                               .Where(MSG => MSG.ToUserId == user.Id)
                               .OrderByDescending(MSG => MSG.DateTimeSent)
                               .Skip(pageHelper.PagesToSkip)
                               .Take(pageHelper.PageSize);

                }
                else
                {
                    numberOfmessages = _DbContext.Messages.Count(MSG => 
                                    MSG.ToUserId == user.Id
                                      && (   MSG.FromUser.Contains(SearchString)
                                          || MSG.Subject.Contains(SearchString)
                                          || MSG.MessageBody.Contains(SearchString)
                                          )
                                                                 );

                    if(numberOfmessages == 0)
                    {
                        return PartialView("_NoMsgsFound");

                    }
                    var pageNumber = PageNav == "search" ? 0 : PageNumber - 1;
                    pageHelper = new PageNavigationHelper(numberOfmessages, 10, pageNumber, PageNav);
                    messages = _DbContext.Messages
                               .Where(MSG =>
                                            MSG.ToUserId == user.Id
                                         && (   MSG.FromUser.Contains(SearchString)
                                             || MSG.Subject.Contains(SearchString)
                                             || MSG.MessageBody.Contains(SearchString)
                                            )
                                      )
                               .OrderByDescending(MSG => MSG.DateTimeSent)
                               .Skip(pageHelper.PagesToSkip)
                               .Take(pageHelper.PageSize);

                }
                

                if (messages != null && messages.Count() > 0)
                {
                    var userPreviewMessagesViewModel = new UserPreviewMessagesViewModel()
                    {
                        UserPreviewMessages = new List<UserPreviewMessage>()
                    };
                    
                    userPreviewMessagesViewModel.PageNumber = pageHelper.CurrentPage + 1;
                    userPreviewMessagesViewModel.isFirstPage = pageHelper.isFirstPage;
                    userPreviewMessagesViewModel.isLastPage = pageHelper.isLastPage;
                    userPreviewMessagesViewModel.NumberOfMessages = numberOfmessages;

                    foreach (var message in messages)
                    {
                        userPreviewMessagesViewModel.UserPreviewMessages.Add(new UserPreviewMessage()
                        {
                            Id = _dataProtector.Protect(message.Id),
                            FromUser = message.FromUser,
                            Subject = message.Subject,
                            MessageBody = message.MessageBody,
                            isRead = message.IsRead,

                        });

                    }

                    return PartialView("_ViewInboxListPartial", userPreviewMessagesViewModel);

                }
                else
                {
                    return PartialView("_InboxEmptyPartial");
                }

            }

            return RedirectToAction("index", "home");
        }



        

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMessages(string[] MessagesToDelete)
        {
            var user = await _MemberManager.GetUserAsync(User);

            await Task.Delay(1000);

            if (user != null)
            {

                if (MessagesToDelete == null || MessagesToDelete.Length == 0)
                {
                    return RedirectToAction("index", "home");
                }
                else
                {
                    try
                    {
                        foreach (var msgID in MessagesToDelete)
                        {
                            var messageToDelete = await _DbContext.Messages.FindAsync(_dataProtector.Unprotect(msgID));
                            if (messageToDelete!=null)
                            {
                                _DbContext.Remove(messageToDelete);
                            }
                        }

                        var result = await _DbContext.SaveChangesAsync();

                        user.UnreadMessages = _DbContext.Messages
                            .Count(MSG =>
                                       MSG.ToUserId == user.Id
                                    && MSG.IsRead == false
                                   );

                        await _DbContext.SaveChangesAsync();

                    }
                    catch (DbUpdateException ex)
                    {
                        //log
                        throw ex;
                    }

                    
                    

                    IQueryable<UserMessage> messages = null;
                    PageNavigationHelper pageHelper = null;
                    int numberOfmessages;

                    numberOfmessages = _DbContext.Messages.Count(MSG => MSG.ToUserId == user.Id);
                    pageHelper = new PageNavigationHelper(numberOfmessages, 10, 0, "Delete");
                    messages = _DbContext.Messages
                               .Where(MSG => MSG.ToUserId == user.Id)
                               .OrderByDescending(MSG => MSG.DateTimeSent)
                               .Skip(pageHelper.PagesToSkip)
                               .Take(pageHelper.PageSize);


                    if (messages != null && messages.Count() > 0)
                    {
                        var userPreviewMessagesViewModel = new UserPreviewMessagesViewModel()
                        {
                            UserPreviewMessages = new List<UserPreviewMessage>()
                        };

                        userPreviewMessagesViewModel.PageNumber = pageHelper.CurrentPage + 1;
                        userPreviewMessagesViewModel.isFirstPage = pageHelper.isFirstPage;
                        userPreviewMessagesViewModel.isLastPage = pageHelper.isLastPage;
                        userPreviewMessagesViewModel.NumberOfMessages = numberOfmessages;

                        foreach (var message in messages)
                        {
                            userPreviewMessagesViewModel.UserPreviewMessages.Add(new UserPreviewMessage()
                            {
                                Id = _dataProtector.Protect(message.Id),
                                FromUser = message.FromUser,
                                Subject = message.Subject,
                                MessageBody = message.MessageBody,
                                isRead = message.IsRead,

                            });

                        }

                        return PartialView("_ViewInboxListPartial", userPreviewMessagesViewModel);
                    }
                    else
                    {
                        return PartialView("_InboxEmptyPartial");
                    }


                }


            }

            return RedirectToAction("index", "home");

        }



















        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReadMessage(string messageId)
        {
            if (string.IsNullOrEmpty(messageId))
            {
                return RedirectToAction("index", "home");
            }
            else
            {
                var user = await _MemberManager.GetUserAsync(User);

                if (user!=null)
                {

                   var message = _DbContext.Messages
                              .Where(MSG => 
                                            MSG.Id == _dataProtector.Unprotect(messageId)                                        
                                         && MSG.ToUserId == user.Id
                                    )
                              .FirstOrDefault();

                    if (message!=null)
                    {
                        //not done
                        var repliedMsgString = "";
                        if (message.RepliedToMsgId!=null)
                        {
                            var repliedMessage = _DbContext.Messages
                              .Where(MSG => MSG.Id == message.RepliedToMsgId)
                              .FirstOrDefault();

                            repliedMsgString = repliedMessage.MessageBody;
                        }

                        message.IsRead = true;

                        await _DbContext.SaveChangesAsync();

                        user.UnreadMessages = _DbContext.Messages
                            .Count(MSG =>
                                       MSG.ToUserId == user.Id
                                    && MSG.IsRead == false
                                   );

                        await _DbContext.SaveChangesAsync();

                        var readMessageViewModel = new ReadMessageViewModel()
                        {
                            MessageId = _dataProtector.Protect(message.Id),
                            RepliedTo = repliedMsgString.Length <31 ? repliedMsgString: "["+repliedMsgString.Substring(0, 30)+"...]",                            
                            DateTimeSent = message.DateTimeSent,
                            FromUser = message.FromUser,
                            Subject = message.Subject,
                            Body = message.MessageBody
                        };


                        return View(readMessageViewModel);


                    }




                }



            }

            return RedirectToAction("index", "home");

        }



        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReadMessage(string MessageIdToDelete)
        {
            if (string.IsNullOrEmpty(MessageIdToDelete))
            {
                return NotFound();
            }
            else
            {
                var user = await _MemberManager.GetUserAsync(User);

                if (user == null)
                {
                    return NotFound();
                }

                try
                {
                    var MsgIdToDelete = _dataProtector.Unprotect(MessageIdToDelete);

                    var messageToDelete = await _DbContext.Messages.FindAsync(MsgIdToDelete);

                    if (messageToDelete.ToUserId==user.Id)
                    {
                        _DbContext.Remove(messageToDelete);
                    }

                    await _DbContext.SaveChangesAsync();

                    user.UnreadMessages = _DbContext.Messages
                            .Count(MSG =>
                                       MSG.ToUserId == user.Id
                                    && MSG.IsRead == false
                                   );

                    await _DbContext.SaveChangesAsync();

                }
                catch (Exception)
                {                    
                    return NotFound();
                }

                return Json("Ok");

            }


        }


        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsUnread(string MessageIdToMarkAsUnread)
        {
            if (string.IsNullOrEmpty(MessageIdToMarkAsUnread))
            {
                return NotFound();
            }
            else
            {
                var user = await _MemberManager.GetUserAsync(User);

                if (user == null)
                {
                    return RedirectToAction("index", "home");
                }


                try
                {
                    var messageIdToMarkAsUnread = _dataProtector.Unprotect(MessageIdToMarkAsUnread);

                    var markedMessage = _DbContext.Messages
                              .Where(MSG => MSG.Id == messageIdToMarkAsUnread)
                              .FirstOrDefault();

                    if (markedMessage!=null)
                    {
                        markedMessage.IsRead = false;

                        await _DbContext.SaveChangesAsync();

                        user.UnreadMessages = _DbContext.Messages
                                .Count(MSG =>
                                           MSG.ToUserId == user.Id
                                        && MSG.IsRead == false
                                       );

                        await _DbContext.SaveChangesAsync();

                        return Json("Ok");

                    }


                }
                catch (Exception)
                {
                    return NotFound();
                }





                


                //try
                //{
                //    var MsgIdToDelete = _dataProtector.Unprotect(MessageIdToMarkAsUnread);

                //    var messageToDelete = await _DbContext.Messages.FindAsync(MsgIdToDelete);

                //    if (messageToDelete.ToUserId == user.Id)
                //    {
                //        _DbContext.Remove(messageToDelete);
                //    }

                //    await _DbContext.SaveChangesAsync();

                //    user.UnreadMessages = _DbContext.Messages
                //            .Count(MSG =>
                //                       MSG.ToUserId == user.Id
                //                    && MSG.IsRead == false
                //                   );

                //    await _DbContext.SaveChangesAsync();

                //}
                //catch (Exception)
                //{
                //    return NotFound();
                //}

                //return Ok();

            }

            return NotFound();

        }







        [HttpGet]
        public async Task<IActionResult> TestPage3()
        {
            return await Task.Run(()=> Content("test page 3"));

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
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
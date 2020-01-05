using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace NutshellRepo.Controllers
{
    public class ErrorHandlerController : Controller
    {
        [Route("Exception")]
        [AllowAnonymous]
        public IActionResult Exception()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionDetails!=null)
            {
                //log it then send something friendly
                ViewBag.ExceptionPath = exceptionDetails.Path;
                ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
                ViewBag.Stacktrace = exceptionDetails.Error.StackTrace;
                return View("Exception");
            }
            //return RedirectToAction("Index", "Home");
            ViewBag.ExceptionPath = "No info";
            ViewBag.ExceptionMessage = "Undefined";
            ViewBag.Stacktrace = "No Details";
            return View("Exception");
            
        }

        [Route("Error/{statusCode}")]
        [AllowAnonymous]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                    {
                        ViewBag.Title = "Bad Request";
                        ViewBag.ErrorMessage = "The server did not understand the request."; break;
                    }
                case 401:
                    {
                        ViewBag.Title = "Unauthorized";
                        ViewBag.ErrorMessage = "The requested page needs a username and a password."; break;
                    }
                case 403:
                    {
                        ViewBag.Title = "Forbidden";
                        ViewBag.ErrorMessage = "Access is forbidden to the requested page."; break;
                    }
                case 404:
                    {
                        ViewBag.Title = "Not Found";
                        ViewBag.ErrorMessage = "The server can not find the requested page."; break;
                    }
                case 405:
                    {
                        ViewBag.Title = "Method Not Allowed";
                        ViewBag.ErrorMessage = "The method specified in the request is not allowed."; break;
                    }
                case 406:
                    {
                        ViewBag.Title = "Not Acceptable";
                        ViewBag.ErrorMessage = "The server can only generate a response that is not accepted by the client."; break;
                    }
                case 407:
                    {
                        ViewBag.Title = "Proxy Authentication Required";
                        ViewBag.ErrorMessage = "You must authenticate with a proxy server before this request can be served."; break;
                    }
                case 408:
                    {
                        ViewBag.Title = "Request Timeout";
                        ViewBag.ErrorMessage = "The request took longer than the server was prepared to wait."; break;
                    }
                case 409:
                    {
                        ViewBag.Title = "Conflict";
                        ViewBag.ErrorMessage = "The request could not be completed because of a conflict."; break;
                    }
                case 410:
                    {
                        ViewBag.Title = "Gone";
                        ViewBag.ErrorMessage = "The requested page is no longer available."; break;
                    }
                case 411:
                    {
                        ViewBag.Title = "Length Required";
                        ViewBag.ErrorMessage = "The \"Content - Length\" is not defined. The server will not accept the request without it."; break;
                    }
                case 412:
                    {
                        ViewBag.Title = "Precondition Failed";
                        ViewBag.ErrorMessage = "The pre condition given in the request evaluated to false by the server."; break;
                    }
                case 413:
                    {
                        ViewBag.Title = "Precondition Failed";
                        ViewBag.ErrorMessage = "The server will not accept the request, because the request entity is too large."; break;
                    }
                case 414:
                    {
                        ViewBag.Title = "Precondition Failed";
                        ViewBag.ErrorMessage = "The server will not accept the request, because the url is too long."; break;
                    }
                case 415:
                    {
                        ViewBag.Title = "Unsupported Media Type";
                        ViewBag.ErrorMessage = "The server will not accept the request, because the mediatype is not supported."; break;
                    }
                case 416:
                    {
                        ViewBag.Title = "Requested Range Not Satisfiable";
                        ViewBag.ErrorMessage = "The requested byte range is not available and is out of bounds."; break;
                    }
                case 417:
                    {
                        ViewBag.Title = "Expectation Failed";
                        ViewBag.ErrorMessage = "The expectation given in an Expect request-header field could not be met by this server."; break;
                    }
                case 429:
                    {
                        ViewBag.Title = "Too Many Requests";
                        ViewBag.ErrorMessage = "Exceeded Requests to this Web site. Try again later."; break;
                    }
                default:
                    {
                        ViewBag.Title = "Undefined Error";
                        ViewBag.ErrorMessage = "Error undefined by the server ;)"; break;
                    }
            }

            return View("Error");
        }
    }
}
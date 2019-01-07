using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace _878876.Controllers
{
    [RequireHttps]
    public class ErrorsController : Controller
    {
        [Route("Error/500")]
        public IActionResult Error500()
        {
            var exceptionType = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if(exceptionType != null)
            {
                ViewBag.ErrorMessage = exceptionType.Error.Message;
                ViewBag.RouteOfException = exceptionType.Path;
            }

            return View();
        }

        [Route("Error/{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            var statusCodeData = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Error 404: Sorry the page you requested could not be found";
                    ViewBag.RouteOfException = statusCodeData.OriginalPath;
                    break;
                case 500:
                    ViewBag.ErrorMessage = "Error 500: Sorry something went wrong on the server";
                    ViewBag.RouteOfException = statusCodeData.OriginalPath;
                    break;
            }

            return View();
        }
    }
}
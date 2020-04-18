using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSessions.Features
{
    public class BaseController : Controller
    {
        protected JsonResult AjaxRedirect(string url)
        {
            return Json(new { redirectUrl = url });
        }

        protected void SuccessMessage(string message)
        {
            Toast(message, "success");
        }

        protected void ErrorMessage(string message)
        {
            Toast(message, "error");
        }

        void Toast(string message, string type)
        {
            TempData["ToastMessage"] = message;
            TempData["ToastType"] = type;
        }
    }
}

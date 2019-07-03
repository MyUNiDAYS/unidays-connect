using System;
using System.Web;
using System.Web.Mvc;

namespace StandardLibrary.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Logout
        public ActionResult Index()
        {
            // Clear crude authentication cookie belonging to this application
            var authCookie = new HttpCookie("auth");
            authCookie.HttpOnly = true;
            authCookie.Expires = DateTime.Today.AddDays(-1);
            HttpContext.Response.Cookies.Set(authCookie);

            return RedirectToAction("Index","Home");
        }
    }
}
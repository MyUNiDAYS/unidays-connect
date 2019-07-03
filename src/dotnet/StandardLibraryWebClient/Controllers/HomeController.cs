using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Client;

namespace StandardLibrary.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            // Read crude authentication cookie belonging to this application
            var authCookie = HttpContext.Request.Cookies.Get("auth");

            ViewBag.Username = authCookie?.Value;

            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using System.Web.Mvc;

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
    }
}
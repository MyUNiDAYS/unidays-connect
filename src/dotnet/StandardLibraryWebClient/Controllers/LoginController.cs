using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Client;

namespace StandardLibrary.Controllers
{
    public class LoginController : Controller
    {
        private readonly string _clientId = WebConfigurationManager.AppSettings["ClientId"];
        private readonly string _returnUrl = WebConfigurationManager.AppSettings["ReturnUrl"];
        private readonly string _openIdServer = WebConfigurationManager.AppSettings["OpenIdServer"];


        // GET: Login
        public ActionResult Index()
        {

            // Create CSRF token and store in http-only cookie
            var state = HttpUtility.UrlEncode(Guid.NewGuid().ToString());
            var stateCookie = new HttpCookie("state", state);
            stateCookie.HttpOnly = true;
            HttpContext.Response.Cookies.Set(stateCookie);

            //Build the authorize Url and redirect the user
            var client = new OAuth2Client(new Uri($"{_openIdServer}/oauth/authorize"));
            string url = client.CreateCodeFlowUrl(
                clientId: _clientId,
                redirectUri: _returnUrl,
                state:state,
                scope: "name email",
                nonce: Guid.NewGuid().ToString());

            return Redirect(url);
        }
    }
}
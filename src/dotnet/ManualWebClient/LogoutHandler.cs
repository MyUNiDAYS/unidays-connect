using System;
using System.Web;

namespace Manual
{
    /// <summary>
    /// Handler to log the current user out
    /// </summary>
    public sealed class LogoutHandler : IHttpHandler
    {
        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            // Clear crude authentication cookie belonging to this application
            var authCookie = new HttpCookie("auth");
            authCookie.HttpOnly = true;
            authCookie.Expires = DateTime.Today.AddDays(-1);
            context.Response.Cookies.Set(authCookie);

            // Redirect to the index
            context.Response.StatusCode = 302;
            context.Response.Headers.Add("Location", "/");
        }
    }
}
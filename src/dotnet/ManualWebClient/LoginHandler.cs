using System;
using System.Web;
using System.Web.Configuration;

namespace ManualWebClient
{
	/// <summary>
	/// Handler to begin the login process
	/// </summary>
	public sealed class LoginHandler : IHttpHandler
	{
		public bool IsReusable => true;

	    private readonly string _clientId = WebConfigurationManager.AppSettings["ClientId"];
		private readonly string _returnUrl = WebConfigurationManager.AppSettings["ReturnUrl"];
	    private readonly string _openIdServer = WebConfigurationManager.AppSettings["OpenIdServer"];


        public void ProcessRequest(HttpContext context)
		{
			// Create CSRF token and store in http-only cookie
			var state = HttpUtility.UrlEncode(Guid.NewGuid().ToString());
			var stateCookie = new HttpCookie("state", state);
			stateCookie.HttpOnly = true;
			context.Response.Cookies.Set(stateCookie);

			// Redirect to the Authorization server `oauth/authorize` endpoint
			context.Response.StatusCode = 302;
			context.Response.Headers.Add("Location"
			    , $"{_openIdServer}/oauth/authorize" +
			      $"?client_id={_clientId}" +
			      $"&response_type=code" +
			      $"&state={state}" + 
				  "&scope=openid offline_access email name verification" + 
			      "&redirect_uri=" + HttpUtility.UrlEncode(_returnUrl));
		}
	}
}
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace PkceWebClient
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
			var codeVerifier = HttpUtility.UrlEncode(Guid.NewGuid().ToString());
			var verifierCookie = new HttpCookie("verifier", codeVerifier);
			verifierCookie.HttpOnly = true;
			context.Response.Cookies.Set(verifierCookie);

			// Redirect to the Authorization server `oauth/authorize` endpoint
			context.Response.StatusCode = 302;
			context.Response.Headers.Add("Location"
			    , $"{_openIdServer}/oauth/authorize" +
			      $"?client_id={_clientId}" +
			      $"&response_type=code" +
			      $"&code_challenge={ComputeSha256Hash(codeVerifier)}" +
				  "&code_challenge_method=S256" +
				  "&scope=openid email name verification offline_access" + 
			      "&redirect_uri=" + HttpUtility.UrlEncode(_returnUrl));
		}

		static string ComputeSha256Hash(string rawData)
		{
			// Create a SHA256   
			using (SHA256 sha = SHA256.Create())
			{
				var bytes = Encoding.ASCII.GetBytes(rawData);
				var hashBytes = sha.ComputeHash(bytes);
				var hash = Convert.ToBase64String(hashBytes, Base64FormattingOptions.None)
					.Replace('+', '-').Replace('/', '_').TrimEnd('=');
				return hash;
			}
		}
	}
}
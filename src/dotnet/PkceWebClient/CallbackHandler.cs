using System;
using System.Collections.Generic;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Configuration;
using Newtonsoft.Json;

namespace PkceWebClient
{
	/// <summary>
	/// Handler to receive the callback from the remote Authorization service
	/// </summary>
	public sealed class CallbackHandler : HttpTaskAsyncHandler
	{
	    private readonly string _clientId = WebConfigurationManager.AppSettings["ClientId"];
	    private readonly string _returnUrl = WebConfigurationManager.AppSettings["ReturnUrl"];
	    private readonly string _openIdServer = WebConfigurationManager.AppSettings["OpenIdServer"];

        public override async Task ProcessRequestAsync(HttpContext context)
		{
			// Receive `code` afrom query string
			var code = context.Request.QueryString["code"];
			var requestVerifierCookie = context.Request.Cookies["verifier"];

			// Clear browser's verifier cookie as its no longer needed
			var responseStateCookie = new HttpCookie("verifier");
			responseStateCookie.HttpOnly = true;
			responseStateCookie.Expires = DateTime.Today.AddDays(-1);
			context.Response.Cookies.Set(responseStateCookie);

			// Pass `code`, `client_id`, `code_verifier` to `/oauth/token` to get an `access_token`
			string accessToken;
			string refreshToken;
			using (var tokenClient = new HttpClient())
			{
				var content = new FormUrlEncodedContent(new Dictionary<string, string>
				{
					{ "grant_type", "authorization_code" },
					{ "code", code },
					{ "code_verifier", requestVerifierCookie.Value },
					{ "client_id", _clientId },
					{ "redirect_uri", _returnUrl }
				});
				var response = await tokenClient.PostAsync($"{_openIdServer}/oauth/token", content);

				// Read the access token from the response, assuming everything worked.
				var responseString = await response.Content.ReadAsStringAsync();
				dynamic tokenJson = JsonConvert.DeserializeObject(responseString);
				accessToken = tokenJson.access_token;
				refreshToken = tokenJson.refresh_token;
			}

			context.Application.Remove("refresh_token");
            context.Application.Add("refresh_token", refreshToken);

			// Pass `access_token` to `/oauth/userinfo` to retrieve user details
			string userId = null;
			string userFirstName = null;
			string userLastName = null;
		    string email = null;
			string userType = null;
			string verified = null;

			using (var userInfoClient = new HttpClient())
			{
				var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_openIdServer}/oauth/userinfo");
				httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
				var response = await userInfoClient.SendAsync(httpRequestMessage);

				// Read the user information from the response, assuming everything worked.
				var responseString = await response.Content.ReadAsStringAsync();
				dynamic userInfoJson = JsonConvert.DeserializeObject(responseString);
				userId = userInfoJson.sub;
				userFirstName = userInfoJson.given_name;
				userLastName = userInfoJson.family_name;
			    email = userInfoJson.email; 
				userType = userInfoJson.verification_status.user_type;
				verified = userInfoJson.verification_status.verified;
			}

			// Log user in using crude authentication cookie belonging to this application
			var stateCookie = new HttpCookie("auth", $" at {DateTime.UtcNow.ToLongTimeString()} Id : `{userId}`, First Name : `{userFirstName}`, Last Name : `{userLastName}`, Email : `{email}`, User type : `{userType}`, Is verified : `{verified}`");
			stateCookie.HttpOnly = true;
			context.Response.Cookies.Set(stateCookie);

			// Go back to the index
			context.Response.StatusCode = 302;
			context.Response.Headers.Add("Location", "/");
		}
	}
}
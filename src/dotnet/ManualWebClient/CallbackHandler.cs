using System;
using System.Collections.Generic;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Configuration;
using Newtonsoft.Json;

namespace Manual
{
	/// <summary>
	/// Handler to receive the callback from the remote Authorization service
	/// </summary>
	public sealed class CallbackHandler : HttpTaskAsyncHandler
	{
	    private readonly string _clientId = WebConfigurationManager.AppSettings["ClientId"];
	    private readonly string _clientSecret = WebConfigurationManager.AppSettings["ClientSecret"];
	    private readonly string _returnUrl = WebConfigurationManager.AppSettings["ReturnUrl"];
	    private readonly string _openIdServer = WebConfigurationManager.AppSettings["OpenIdServer"];

        public override async Task ProcessRequestAsync(HttpContext context)
		{
			// Receive `code` and `state` from query string
			var code = context.Request.QueryString["code"];
			var state = context.Request.QueryString["state"];

			// Clear browser's state cookie as its no longer needed
			var responseStateCookie = new HttpCookie("state");
			responseStateCookie.HttpOnly = true;
			responseStateCookie.Expires = DateTime.Today.AddDays(-1);
			context.Response.Cookies.Set(responseStateCookie);

			// Validate CSRF state
			var requestStateCookie = context.Request.Cookies["state"];
			if (requestStateCookie?.Value == null || state != requestStateCookie.Value)
			{
				context.Response.StatusCode = 400;
				context.Response.Write(@"Bad state");
				return;
			}

			// Pass `code`, `client_id`, `client_secret` to https://account.unidays.ab.dev/oauth/token to get an `access_token`
			string accessToken;
			using (var tokenClient = new HttpClient())
			{
				var content = new FormUrlEncodedContent(new Dictionary<string, string>
				{
					{ "grant_type", "authorization_code" },
					{ "code", code },
					{ "client_id", _clientId },
					{ "client_secret", _clientSecret },
					{ "scope", "user:name" },
					{ "redirect_uri", _returnUrl }
				});
				var response = await tokenClient.PostAsync($"{_openIdServer}/oauth/access_token", content);

				// Read the access token from the response, assuming everything worked.
				var responseString = await response.Content.ReadAsStringAsync();
				dynamic tokenJson = JsonConvert.DeserializeObject(responseString);
				accessToken = tokenJson.access_token;
			}

			// Pass `access_token` to https://account.unidays.ab.dev/oauth/userinfo to retrieve user details
			string userId = null;
			string userFirstName = null;
			string userLastName = null;
		    string email = null;
		    bool verified = false;
		    string userType = null;

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
			    verified = userInfoJson.verification_status.verified;
			    userType = userInfoJson.verification_status.user_type;

            }

			// Log user in using crude authentication cookie belonging to this application
			var stateCookie = new HttpCookie("auth", userId + ", " + userFirstName + " " + userLastName + " " + email + " "+verified+ " "+userType);
			stateCookie.HttpOnly = true;
			context.Response.Cookies.Set(stateCookie);

			// Go back to the index
			context.Response.StatusCode = 302;
			context.Response.Headers.Add("Location", "/");
		}
	}
}
using System.Collections.Generic;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Configuration;
using Newtonsoft.Json;
using System;

namespace ManualWebClient
{
	/// <summary>
	/// Handler get a new access token using refresh token
	/// </summary>
	public sealed class RefreshHandler : HttpTaskAsyncHandler
	{
	    private readonly string _clientId = WebConfigurationManager.AppSettings["ClientId"];
		private readonly string _clientSecret = WebConfigurationManager.AppSettings["ClientSecret"];
		private readonly string _returnUrl = WebConfigurationManager.AppSettings["ReturnUrl"];
	    private readonly string _openIdServer = WebConfigurationManager.AppSettings["OpenIdServer"];

        public override async Task ProcessRequestAsync(HttpContext context)
		{
			// Receive `refresh_token` from query string
			var refreshTokenQuery = context.Request.QueryString["refresh_token"];
            string accessToken;
            string refreshToken;

			using (var tokenClient = new HttpClient())
			{
				var content = new FormUrlEncodedContent(new Dictionary<string, string>
				{
					{ "grant_type", "refresh_token" },
					{ "refresh_token", refreshTokenQuery },
					{ "client_id", _clientId },
					{ "client_secret", _clientSecret }
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
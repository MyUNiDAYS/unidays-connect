using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Client;

namespace StandardLibrary.Controllers
{
    public class CallbackController : Controller
    {
        private readonly string _clientId = WebConfigurationManager.AppSettings["ClientId"];
        private readonly string _clientSecret = WebConfigurationManager.AppSettings["ClientSecret"];
        private readonly string _returnUrl = WebConfigurationManager.AppSettings["ReturnUrl"];
        private readonly string _openIdServer = WebConfigurationManager.AppSettings["OpenIdServer"];


        // GET: Callback
        public async Task<ActionResult> Index(string code, string state, string error, string errorDescription)
        {
            //If any errors we go back to the home screen to try again
            if (!String.IsNullOrEmpty(error))
                return RedirectToAction("Index", "Home");


            // Validate CSRF state, it needs to match
            var requestStateCookie = HttpContext.Request.Cookies["state"];
            if (requestStateCookie?.Value == null || state != requestStateCookie.Value)
                return RedirectToAction("Index", "Home");

            // Clear browser's state cookie as its no longer needed
            var stateCookie = new HttpCookie("state");
            stateCookie.HttpOnly = true;
            stateCookie.Expires = DateTime.Today.AddDays(-1);
            HttpContext.Response.Cookies.Set(stateCookie);

            //Build a client and request the access token
            var client = new OAuth2Client(new Uri($"{_openIdServer}/oauth/token"), _clientId, _clientSecret);
            var tokenResponse = await client.RequestAuthorizationCodeAsync(
                code: code,
                redirectUri: _returnUrl,
                additionalValues:new Dictionary<string, string>()
                {
                    {"client_id",_clientId },
                    {"client_secret", _clientSecret }
                }
            );

            //If any errors we go back to the home screen to try again
            if (tokenResponse.IsError)
                return RedirectToAction("Index", "Home");

            //Access the user info endpoint to get the sub
            var token = tokenResponse.AccessToken;
            var rToken = tokenResponse.RefreshToken;

            //Get user info
            var allUserInfo = await this.CallUserInfo(token);

            // Log user in using crude authentication cookie belonging to this application
            var authCookie = new HttpCookie("auth", allUserInfo);

            authCookie.HttpOnly = true;
            HttpContext.Response.Cookies.Set(authCookie);

            //Exchange refresh token
            var exchangeResponse = await client.RequestRefreshTokenAsync(
               rToken,
                additionalValues: new Dictionary<string, string>()
                {
                    {"client_id",_clientId },
                    {"client_secret", _clientSecret }
                }
            );

            if (exchangeResponse.IsError)
                return RedirectToAction("Index", "Home");

            //Access the user info endpoint to get the sub
            token = exchangeResponse.AccessToken;
            rToken = exchangeResponse.RefreshToken;
           
            allUserInfo = await this.CallUserInfo(token);

            return RedirectToAction("Index", "Home");
        }

        private async Task<string> CallUserInfo(string token)
        {
            var userInfoEP = new UserInfoClient(new Uri($"{_openIdServer}/oauth/userinfo"), token);
            var userInfoResponse = await userInfoEP.GetAsync();
            var userSub = userInfoResponse.JsonObject["sub"].ToString();

            //Parse response to get requested scopes
            var userGivenName = userInfoResponse.JsonObject["given_name"].ToString();
            var userFamilyName = userInfoResponse.JsonObject["family_name"].ToString();
            var userEmail = userInfoResponse.JsonObject["email"].ToString();
            var userType = userInfoResponse.JsonObject["verification_status"]["user_type"].ToString();
            var verified = userInfoResponse.JsonObject["verification_status"]["verified"].ToString();

            // Log user in using crude authentication cookie belonging to this application
            return $" at {DateTime.UtcNow.ToLongTimeString()} Id : `{userSub}`, First Name : `{userGivenName}`, Last Name : `{userFamilyName}`, Email : `{userEmail}`, User type : `{userType}`, Is verified : `{verified}`";
        }
    }
}
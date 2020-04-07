using System.Web;

namespace PkceWebClient
{
	/// <summary>
	/// Handler for the index page for the application. Renders either a login button, or a welcome message to the logged in user
	/// </summary>
	public sealed class IndexHandler : IHttpHandler
	{
		public bool IsReusable => true;

		public void ProcessRequest(HttpContext context)
		{
			// We're going to return HTML
			context.Response.ContentType = "text/html";

			// Read crude authentication cookie belonging to this application
			var authCookie = context.Request.Cookies.Get("auth");

            var refresh_token = (string)context.Application.Get("refresh_token");
			
			// If we're logged in
			if (authCookie?.Value != null && !string.IsNullOrEmpty(refresh_token))
			{
				// Render welcome message
				context.Response.Write($@"
<html>
<body>
	<p>Hello {authCookie.Value}</p>
	<a href=""/refresh?refresh_token={refresh_token}"">Use Refresh Token</a>
	</br>
	<a href=""/log-out"">Log Out</a>
</body>
</html>
");
				return;
			}

			// We're not logged in, render Log In link
			context.Response.Write($@"
<html>
<body>
	<a href=""/log-in""><img src=""/login-Green.png""></img></a>
</body>
</html>
");
		}
    }
}
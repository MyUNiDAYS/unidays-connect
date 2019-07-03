using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace CoreWebMicrosoftMiddleware
{
	public sealed class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});
			
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
					options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = "UNiDAYS";
				})
				.AddCookie()
				.AddOAuth("UNiDAYS", options =>
				{
					options.ClientId = Configuration["UNiDAYS:ClientId"];
					options.ClientSecret = Configuration["UNiDAYS:ClientSecret"];
					options.CallbackPath = new PathString(Configuration["UNiDAYS:ReturnUrl"]);

					options.AuthorizationEndpoint = $"{Configuration["UNiDAYS:OpenIdServer"]}/oauth/authorize";
					options.TokenEndpoint = $"{Configuration["UNiDAYS:OpenIdServer"]}/oauth/access_token";
					options.UserInformationEndpoint = $"{Configuration["UNiDAYS:OpenIdServer"]}/oauth/userinfo";

				    options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "sub");

                    options.Events = new OAuthEvents
					{
						OnCreatingTicket = async context =>
						{
							var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
							request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
							request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

							var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
							response.EnsureSuccessStatusCode();

							var user = JObject.Parse(await response.Content.ReadAsStringAsync());

							context.RunClaimActions(user);
						}
					};
				});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseDeveloperExceptionPage();

			app.UseAuthentication();

			app.UseCookiePolicy();

			app.UseMvc(routes =>
			{
				routes.MapRoute(name: "default", template: "{controller}/{action=Index}/{id?}");
			});
		}
	}
}
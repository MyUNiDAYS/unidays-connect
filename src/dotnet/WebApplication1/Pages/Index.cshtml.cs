using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
	public class IndexModel : PageModel
	{
		public string GitHubAvatar { get; set; }

		public string GitHubLogin { get; set; }

		public string GitHubName { get; set; }

		public string GitHubUrl { get; set; }

		public void OnGet()
		{
			if (User.Identity.IsAuthenticated)
			{
				GitHubName = User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;
				GitHubLogin = User.FindFirst(c => c.Type == "urn:github:login")?.Value;
				GitHubUrl = User.FindFirst(c => c.Type == "urn:github:url")?.Value;
				GitHubAvatar = User.FindFirst(c => c.Type == "urn:github:avatar")?.Value;
			}
		}
	}
}
